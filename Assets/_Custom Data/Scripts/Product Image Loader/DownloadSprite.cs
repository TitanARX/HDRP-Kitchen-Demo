using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class DownloadSprite : MonoBehaviour
{
    [SerializeField]
    private string m_filename;
    public string Filename
    {
        get
        {
            return m_filename;
        }
        set
        {
            //TODO: check value for validity
            m_filename = value;
        }
    }

    public Image m_img;

    public Texture2D m_loadedTexture = null;

    void Awake()
    {
        m_img = gameObject.GetComponent<Image>();
    }
}
