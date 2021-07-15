using Com.FastEffect.DataTypes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProductLabel : MonoBehaviour
{
    private Animator animator = null;
    [SerializeField]
    private bool isToggleActive = false;

    public Toggle toggle = null;
    public string showTagAnimationName = "Show Tag";
    public string hideTageAnimationName = "Hide Tag";

    private void Awake()
    {
        animator = GetComponent<Animator>();
        isToggleActive = toggle.isOn;
    }

    public void ShowTag()
    {
        if (toggle)
        {
            isToggleActive = toggle.isOn;

            if (isToggleActive)
            {
                ResetTagAnimations();

                return;
            }
            else
            {
                animator.ResetTrigger("Reset Tag");
                animator.ResetTrigger(hideTageAnimationName);
                animator.SetTrigger(showTagAnimationName);
            }
        }
        else
        {
            Debug.Log("toggle not assigned to Product Label on HotSpot " + transform.parent.name);
        }
    }

    public void HideTag()
    {
        if (toggle)
        {
            isToggleActive = toggle.isOn;

            if (isToggleActive)
            {
                ResetTagAnimations();

                return;
            }
            else
            {
                animator.ResetTrigger("Reset Tag");
                animator.ResetTrigger(showTagAnimationName);
                animator.SetTrigger(hideTageAnimationName);
            }
        }
        else
        {
            Debug.Log("toggle not assigned to Product Label on HotSpot " + transform.parent.name);
        }
    }

    public void ResetTagAnimations()
    {
        animator.ResetTrigger(showTagAnimationName);
        animator.ResetTrigger(hideTageAnimationName);
        animator.SetTrigger("Reset Tag");
    }


}
