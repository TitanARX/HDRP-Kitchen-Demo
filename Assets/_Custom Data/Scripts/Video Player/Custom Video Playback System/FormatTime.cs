using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using System;
using UnityEngine.UI;
using TMPro;

public abstract class FormatTime : MonoBehaviour
{
    [SerializeField]
    private float currentRuntime;

    [SerializeField]
    private float maxRuntime;

    private string convertedOutputA;
    private string convertedOutputB;

    private string combinedConvertedOutput;

    public TextMeshProUGUI output;

    public virtual void SetURLDuration(VideoPlayer player)
    {
        //This only happens once after Prepared when using URL
        maxRuntime = (float)player.length;

        string min = Mathf.Floor(maxRuntime / 60).ToString("00");
        string sec = Mathf.RoundToInt(maxRuntime % 60).ToString("00"); ;

        //Debug.Log("Current Max RunTime is " + min + ":" + sec);

        convertedOutputB = min + ":" + sec;
    }

    public virtual void ReportVideoTimeStamp(VideoPlayer player)
    {
        if (player == null)
        {
            return;
        }
        else
        { 
             if(!player.isPlaying)
             {
                    return;
             }
             else
             {
                if (output != null)
                {
                    //This only happens once after Prepared when using URL
                    maxRuntime = (float)player.length;

                    string min = Mathf.Floor(maxRuntime / 60).ToString("00");
                    string sec = Mathf.RoundToInt(maxRuntime % 60).ToString("00"); ;

                    //Debug.Log("Current Max RunTime is " + min + ":" + sec);

                    convertedOutputB = min + ":" + sec;



                    currentRuntime = (float)player.time;

                    string minutes = Mathf.Floor(currentRuntime / 60).ToString("00");
                    string seconds = Mathf.RoundToInt(currentRuntime % 60).ToString("00"); ;

                    convertedOutputA = minutes + ":" + seconds;               

                    //Debug.Log("Current Combined TimeStamp is " + convertedOutputA);

                    combinedConvertedOutput = (convertedOutputA + " / " + convertedOutputB );

                    output.text = combinedConvertedOutput.ToString();
  
                }
            }
        }  
    }
}
