using System.Collections;
using System.Collections.Generic;
using UI.Pagination;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.Video;

public class PagedRectVideoInitializer : MonoBehaviour
{
    private PagedRect pagedRect;
    private Page currentPage;

    public Toggle videoToggle = null;
    public bool videoPanelActive = false;

    public UnityEvent Active;
    public UnityEvent InActive;

    public void CheckVideoPanelState()
    {         
        pagedRect = GetComponent<PagedRect>(); 
        
        videoPanelActive = videoToggle.isOn;

        if (videoPanelActive)
        {
            Page page = pagedRect.GetCurrentPage();

            VideoPlayer videoPlayer = page.GetComponentInChildren<VideoPlayer>();

            videoPlayer.frame = 0;
            
            videoPlayer.Play();

            Active.Invoke();

            Debug.Log("Video Panel Active. Current Page Set To " + pagedRect.CurrentPage);
        }
        else
        {
            InActive.Invoke();

            Debug.Log("Video Panel Inactive. Showing First Page " + pagedRect.CurrentPage);
        }

        Debug.Log("Video Panel Broken");

    }

}
