using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Hotspot : MonoBehaviour
{
    [Space]
    public bool canClick;
    [Space]
    [Header("Delay Between Events Calls")]
    public float delay;
    [Space]
    [Header("Events Fired On Initial Toggle Press")]
    public UnityEvent OnClickEvents;
    [Space]
    [Header("Delayed Events Fired After Initial Toggle Press")]
    public UnityEvent DelayedOnClickEvents;

    #region private vars
    private Toggle hotSpotToggle;
    #endregion

    private void Awake()
    {
        hotSpotToggle = GetComponent<Toggle>();
        hotSpotToggle.onValueChanged.AddListener((bool value) => OnClickClcyeStart());
    }

    public void OnClickClcyeStart()
    {
        canClick = hotSpotToggle.isOn;

        if(!canClick)
        {
            StartCoroutine(OnClickCycle());
        }
    }

    public IEnumerator OnClickCycle()
    {
        canClick = false;

        OnClickEvents.Invoke();

        yield return new WaitForSeconds(delay);

        DelayedOnClickEvents.Invoke();

        canClick = true;
    }



}
