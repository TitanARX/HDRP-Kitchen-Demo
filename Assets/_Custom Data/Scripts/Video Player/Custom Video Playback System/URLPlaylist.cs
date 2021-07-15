using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class URLPlaylist : MonoBehaviour
{
    [Tooltip("Scriptable Object that holds the title and links for videos associated with this player..")]
    [Header("URL Scriptable Object")]
    public CustomURLContainer Playlist;
    [Space]
    [Tooltip("The button prefab that will be used to play videos..")]
    [Header("Button Prefab Reference Object")]
    public Button videoButtonPrefab;
    [Space]
    [Tooltip("The UI Object that will hold the dynamically created button instances..")]
    [Header("Button Container Parent")]
    public Transform videoButtonContainter;

    #region Hidden From Inspector
    [HideInInspector]
    public List<Button> PlaylistButtons = new List<Button>();
    [HideInInspector]
    public int currentIndex = 0;
    [HideInInspector]
    public string currentURL = "";
    #endregion

    public virtual void InitializePlaylist()
    {
        if (Playlist.CustomURLs == null)
            return;

        currentIndex = 0;
        currentURL = Playlist.CustomURLs[currentIndex].URL;        

        PopulateButtonContainer();
    }

    public virtual void PopulateButtonContainer()
    {
        for (int i = 0; i < Playlist.CustomURLs.Count; i++)
        {
            Button buttonInstance = Instantiate(videoButtonPrefab, videoButtonContainter.transform.position, Quaternion.identity);

            buttonInstance.transform.parent = videoButtonContainter;
            buttonInstance.transform.localScale = Vector2.one;

            Text videoName = buttonInstance.GetComponentInChildren<Text>();

            videoName.text = Playlist.CustomURLs[i].FileName;

            PlaylistButtons.Add(buttonInstance);
        }
    }



}
