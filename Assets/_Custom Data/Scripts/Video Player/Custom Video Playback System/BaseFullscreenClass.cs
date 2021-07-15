using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(CanvasGroup))]
public class BaseFullscreenClass : MonoBehaviour
{
    private CanvasGroup GetCanvasGroup;
    public RawImage VideoImage;
    private bool visible;

    public void Start()
    {
        InitFullScreenObject();
    }

    public void InitFullScreenObject()
    {
        GetCanvasGroup = GetComponent<CanvasGroup>();

        GetCanvasGroup.alpha = 0;

        GetCanvasGroup.interactable = false;
    }

    public virtual void FullScreenMode(bool active, Material m)
    {
        if (!active)
            return;

        if (!visible)
        {
            visible = true;

            VideoImage.material = m;

            GetCanvasGroup.DOFade(1f, 0.5f);
        }
        else
        {
            HideFullscreen();
        }
    }

    public void HideFullscreen()
    {
        visible = false;
        GetCanvasGroup.DOFade(0f, 0.5f);
    }

}
