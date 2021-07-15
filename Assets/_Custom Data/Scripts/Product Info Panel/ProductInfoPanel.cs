using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using UnityEngine.UI;


public class ProductInfoPanel : MonoBehaviour
{
    public RectTransform backgroundPanel = null;

    [Header("Product Hotspot Toggle - Worldspace")]
    [Tooltip("Worldspace Toggle in scene used to show/hide this product info panel")]
    public Toggle productHotSpot;

    [Space]
    [Header("Animation Control Parameters")]
    [Range(0f, 10f)]
    public float panelTransitionTime = 0;
    [Space]
    public float panel_X_StartPoint = 0;
    [Space]
    public float panel_X_EndPoint = 0;

    [Space]
    [Header("Callback Events")]
    [Space]
    public UnityEvent PanelOpenEvent;
    [Space]
    public UnityEvent PanelCloseEvent;

    #region private vars
    private CanvasGroup canvasGroup;
    private float canvasGroupSpeed = 1.75f;
    private Sequence openSequence;
    private Sequence closeSequence;
    #endregion

    private void OnEnable()
    {
        productHotSpot.onValueChanged.AddListener(ShowHidePanel);

        canvasGroup = this.transform.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        canvasGroupSpeed = 1.75f;

        canvasGroup.blocksRaycasts = false;

    }

    public void AnimateToggle(Toggle currentToggle)
    {
        bool currentToggleState = currentToggle.isOn;

        if(currentToggleState)
        {
            AnimatePanelOpen();
        }
        else
        {
            AnimatePanelClosed();
        }
    }

    public void AnimatePanelOpen()
    {
        RectTransform _rect = backgroundPanel.GetComponent<RectTransform>();

        _rect.DOSizeDelta(new Vector2(panel_X_EndPoint, _rect.transform.localPosition.y), panelTransitionTime, false);

    }

    public void AnimatePanelClosed()
    {
        RectTransform _rect = backgroundPanel.GetComponent<RectTransform>();

        _rect.DOSizeDelta(new Vector2(panel_X_StartPoint, _rect.transform.localPosition.y), panelTransitionTime, false);
    }

    public void ShowHidePanel(bool b = false)
    {
        bool currentToggleState = productHotSpot.isOn;

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
        openSequence = DOTween.Sequence();

        transform.SetAsFirstSibling();

        canvasGroup.blocksRaycasts = true;

        canvasGroup.interactable = true;

        openSequence.Append(canvasGroup.DOFade(1, 1.75f));

        PanelOpenEvent.Invoke();
    }

    public void ClosePanel()
    {
        openSequence.SetAutoKill(false);

        openSequence.Kill(false);

        closeSequence = DOTween.Sequence();

        canvasGroup.interactable = false;

        canvasGroup.blocksRaycasts = false;

        canvasGroup.DOFade(0, 0.75f);

        PanelCloseEvent.Invoke();
    }

    public void CloseButton()
    {
        productHotSpot.isOn = false;

        AnimatePanelClosed();

        ShowHidePanel();
    }

}
