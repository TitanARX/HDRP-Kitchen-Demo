using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;

[RequireComponent(typeof( VideoPlayer))]
public class EventBasedVideoPlayer : FormatTime
{
    public enum ContentLoadMethod { Web, Resource };
    [Header("Method of Loading Content")]
    public ContentLoadMethod contentLoadMethod;

    #region private vars
    
    private VideoPlayer player;
    private float currentTime = 0f;
    private bool m_wasLooping = false;

    [SerializeField]
    private string contentResourceName = "VideoName";
    [SerializeField]
    private readonly string videoLocationURL = "";
    #endregion

    [Header("Events Called After Video is Prepared")]
    public UnityEvent OnPreparedEvents;
    [Header("Events Called After Video Started")]
    public UnityEvent OnPlayEvents;
    [Header("Use this component's StopPlayerWithEvent() to Stop Video - WIP")]
    public UnityEvent OnStopEvent;

    private void Update()
    { 
        if (player.isPlaying)
        {
            Debug.Log("Playyin");

            ReportVideoTimeStamp(player);
        }
        else if(player.isPrepared)
        {
            SetURLDuration(player);
        }
    }

    private void OnEnable()
    {
        player = GetComponent<VideoPlayer>();

        player.Prepare();

        player.playOnAwake = false;

        player.isLooping = true;

        switch (contentLoadMethod)
        {
            case ContentLoadMethod.Web:

                player.source = VideoSource.Url;

                player.url = videoLocationURL + contentResourceName;

                break;

            case ContentLoadMethod.Resource:

                player.source = VideoSource.VideoClip;

                VideoClip clip = Resources.Load<VideoClip>("Videos/" + contentResourceName) as VideoClip;

                player.clip = clip;

                break;
            default:
                break;
        }


        player.prepareCompleted += VideoPlaybackPrepared;
        player.started += VideoPlaybackStarted;
        player.loopPointReached += VideoPlaybackEnded;
    }


    public void UnregisterVideoPlayer()
    {
        player.prepareCompleted -= VideoPlaybackPrepared;
        player.started -= VideoPlaybackStarted;
        player.loopPointReached -= VideoPlaybackEnded;

    }

    public void Reset()
    {
        player.isLooping = m_wasLooping;
        currentTime = 0f;
        player.frame = 0;
    }


    public void VideoPlaybackPrepared(UnityEngine.Video.VideoPlayer vp)
    {
        vp = player;

        OnPreparedEvents.Invoke();
    }

    public void VideoPlaybackStarted(UnityEngine.Video.VideoPlayer vp)
    {
        vp = player;

        OnPlayEvents.Invoke();
    }

    public void VideoPlaybackEnded(UnityEngine.Video.VideoPlayer vp)
    {
        vp = player;

        if(!m_wasLooping)
        {
            player.Pause();
        }

        OnStopEvent.Invoke();
    }
    public void StopPlayerWithEvent()
    {
        if (player.frame > 5)
        {
            player.Pause();
            player.frame = 0;
            //player.targetTexture.Release();
            OnStopEvent.Invoke();
        }
    }

    private float GetPlayerPercentage()
    {
        return (float)player.time;
    }
}
