using System.Collections;
using System.Collections.Generic;
using UI.Pagination;
using UnityEngine;


public class Debugger : MonoBehaviour
{
    public PagedRect rectP;

    public GameObject SceneIDMapStray = null;

    // Start is called before the first frame update
    void Start()
    {
        SceneIDMapStray = GameObject.Find("SceneIDMap");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(rectP.GetCurrentPage());

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            rectP.SetCurrentPage(2);
        }
    }
}
