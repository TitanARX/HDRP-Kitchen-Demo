using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SequentialImageLoader : MonoBehaviour
{
    public enum ContentLoadMethod{ Web, Resource };

    public ContentLoadMethod contentLoadMethod;

    public bool m_autoStart = true;

    public bool loadingSprite = false;

    [SerializeField]
    private string m_baseURL = "";

    public List<Image> GetDownloadSprites = new List<Image>();

    void OnEnable()
    {
        if (m_autoStart)
        {
            switch (contentLoadMethod)
            {
                case ContentLoadMethod.Web:

                    BeginWebDownload();

                    break;
                case ContentLoadMethod.Resource:

                    BeginResourceLoad();

                    break;
                default:
                    break;
            }
        }
    }

    public void BeginResourceLoad()
    {
        foreach (Image img in GetDownloadSprites)
        {
            DownloadSprite downloadSprite = img.GetComponent<DownloadSprite>();

            string m_filename = downloadSprite.Filename;

            img.sprite = Resources.Load<Sprite>("Images/" + m_filename);
        }
    }

    public void BeginWebDownload()
    {
        StartCoroutine(DownloadImage());
    }

    IEnumerator DownloadImage()
    {
        int index = 0;

        while (!loadingSprite)
        {
            loadingSprite = true;

            DownloadSprite downloadSprite = GetDownloadSprites[index].GetComponent<DownloadSprite>();

            string m_filename = downloadSprite.Filename;

            string MediaUrl = m_baseURL + m_filename;

            UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);

            yield return request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError)
            {
                Debug.LogError(request.error);
            }
            else
            {
                Image m_img = GetDownloadSprites[index];

                Texture2D tex = ((DownloadHandlerTexture)request.downloadHandler).texture;

                yield return request.isDone;

                // using the file's existing width/height
                m_img.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
                m_img.preserveAspect = true;

                if (index != GetDownloadSprites.Count - 1)
                {
                    index++;

                    Debug.LogErrorFormat("Sprite Loaded " + Time.time);

                    loadingSprite = false;
                }
                else
                {
                    m_autoStart = false;
                }

            }
        }
    }

}
