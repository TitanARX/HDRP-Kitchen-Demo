using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
public class URLVideoPlaylist : MonoBehaviour
{
    private VideoPlayer m_video = null;
    [SerializeField]
    private bool m_autoStart = false;
    [SerializeField]
    [Min(0)]private int m_currentIndex = 0;
    [SerializeField]
    private string m_URLPrefix = string.Empty;
    [SerializeField]
    private List<string> m_playlist = new List<string>();

    void Awake()
    {
        m_video = GetComponent<VideoPlayer>();
    }

    void OnEnable()
    {
        if(m_autoStart)
        {
            PlayIndex(m_currentIndex);
        }
        else if(m_playlist.Count > 0)
        {
            m_currentIndex %= m_playlist.Count;
            m_video.url = m_URLPrefix + m_playlist[m_currentIndex];
        }
    }
    public void PlayIndex(int index)
    {
        if(m_playlist.Count > 0)
        {
            index %= m_playlist.Count;
            m_video.url = m_URLPrefix + m_playlist[index];
            m_currentIndex = index;
            m_video.Play();
        }
    }

    public void PlayNext()
    {
        if(m_playlist.Count > 0)
        {
            m_currentIndex = (m_currentIndex+1)%m_playlist.Count;
            m_video.url = m_URLPrefix + m_playlist[m_currentIndex];
            m_video.Play();
        }
    }

    public void PlayPrevious()
    {
        if(m_playlist.Count > 0)
        {
            m_currentIndex = (m_currentIndex-1)%m_playlist.Count;
            m_video.url = m_URLPrefix + m_playlist[m_currentIndex];
            m_video.Play();
        }
    }
}
