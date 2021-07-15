using System.Collections;
using System.Collections.Generic;
using UI.Pagination;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;

public class ContentDisplay : MonoBehaviour
{
    [Header("Product Hotspot Toggle - Worldspace")]
    [Tooltip("Toggle responsible for visibility of content")]
    public Toggle contentPopUpToggle;

    [Space]
    [Header("Animation Delay Parameters")]
    [Range(0f, 10f)]
    public float showDelay = 1.75f;

    [Space]
    [Header("Callback Events")]
    [Space]
    public UnityEvent PanelOpenEvent;
    [Space]
    public UnityEvent PanelCloseEvent;

    #region private vars
    private CanvasGroup canvasGroup;
    private Sequence showSequence;
    private PagedRect pagedRect;
    #endregion

    public void Awake()
	{
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup = this.transform.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;

        canvasGroup.blocksRaycasts = false;

        if (!contentPopUpToggle)
        {
            Debug.Log(this.transform.parent.name + " is missing its toggle reference");
            return;
        }
        else
        {
            contentPopUpToggle.onValueChanged.AddListener((bool value) => ShowHidePanel());
        }
	}


    public void ShowHidePanel()
    {
        bool currentToggleState = contentPopUpToggle.isOn;

        if (!currentToggleState)
        {
            ClosePanel();
        }
        else
        {
            OpenPanel();
        }
    }

    public void OpenPanel()
    {
        showSequence = DOTween.Sequence();

        transform.SetAsFirstSibling();

        canvasGroup.blocksRaycasts = true;

        canvasGroup.interactable = true;

        showSequence.Append(canvasGroup.DOFade(1, showDelay));

        showSequence.OnComplete(PanelOpenEvent.Invoke);
    }

    public void ClosePanel()
    {
        showSequence.SetAutoKill(false);

        showSequence.Kill(false);

        canvasGroup.interactable = false;

        canvasGroup.blocksRaycasts = false;

        canvasGroup.alpha = 0f;

        PanelCloseEvent.Invoke();
    }

    public void CloseButton()
    {
        contentPopUpToggle.isOn = false;

        ClosePanel();
    }
}
