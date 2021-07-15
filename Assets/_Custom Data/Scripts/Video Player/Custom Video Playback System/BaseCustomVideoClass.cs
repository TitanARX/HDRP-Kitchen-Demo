using System.Collections;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using Com.FastEffect.Events;

[RequireComponent(typeof(VideoPlayer))]
public abstract class BaseCustomVideoClass : URLPlaylist ,ICustomVideoClass
{
    protected VideoPlayer player;
    private BaseFullscreenClass FullScreenObj;
    private bool isPrepared = false;
    public bool IsPrepared{get=>isPrepared;}
    private bool isPlaying = false;
    public bool IsPlaying{get=>isPlaying;}

    [Space]
    [Tooltip("The 3D Gameobject that the video will be displayed on...")]
    [Header("3D Screen Mesh")]
    public MeshRenderer screen;

    [Space]
    [Tooltip("Collection of materials that will be used to show either current video, idle or loading image..")]
    [Header("Screen Textures")]
    public ScreenTextureContainer ScreenTextureContainer;

    [Space]
    [Tooltip("Fullscreen UI Button Toggle")]
    public Button FullscreenToggle;

    public void Start()
    {
        FullScreenObj = GameObject.FindObjectOfType<BaseFullscreenClass>();

        VideoplayerInit();
    }

    #region Set Hold, Loading and Current Video Texture
    public void SetScreenTexture(int i)
    {
        Material[] materials = screen.materials;

        materials[0] = ScreenTextureContainer.ScreenTextures[i];

        screen.materials = materials;
    }
    #endregion

    #region Initialize Video Player Features and Controls
    public virtual void VideoplayerInit()
    {
        InitializePlaylist();

        player = GetComponent<VideoPlayer>();

        player.playOnAwake = false;
        player.isLooping = true;

        isPlaying = false;
        isPrepared = false; 

        SetButtonURLRef();

        SetScreenTexture(0);
    }

    public void SetButtonURLRef()
    {
        FullscreenToggle.onClick.AddListener(delegate { OnFullScreen(isPlaying, ScreenTextureContainer.ScreenTextures[0]); });

        for (int i = 0; i < Playlist.CustomURLs.Count; i++)
        {
            string capturedVar = Playlist.CustomURLs[i].URL;

            Debug.Log(capturedVar);

            PlaylistButtons[i].onClick.AddListener(delegate { PlayButtonURL(capturedVar); });
            StringInvoker sInvoker = null;
            if(PlaylistButtons[i].TryGetComponent(out sInvoker))
            {
                //sInvoker.m_value = capturedVar;
            }
        }

        
    }
    #endregion

    #region Play and Stop Methods
    public void PlayButtonURL(string i)
    {
        if (!isPlaying)
        {
            StartCoroutine(Prepare(i));
        }
        else
        {
            Stop();

            StartCoroutine(Prepare(i));
        }
    }


    public virtual IEnumerator Prepare(string i)
    {
        SetScreenTexture(2);

        isPrepared = false;

        player.url = i;

        player.Prepare();

        while (!player.isPrepared)
        {
            yield return null;
        }

        isPrepared = true;

        SetScreenTexture(1);

        player.Play();

        isPlaying = true;
    }

    public virtual void Stop()
    {
        if (isPlaying)
        {
            player.Stop();
            player.frame = 0;

            isPlaying = false;
            isPrepared = false;
        }
    }
    #endregion

    public void OnFullScreen(bool b, Material m)
    {
        if(FullScreenObj != null)
        {
            FullScreenObj.FullScreenMode(b, m);
        }
    }

}
