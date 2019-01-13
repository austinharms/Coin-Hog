using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class start_menu : MonoBehaviour
{
    private Scene scene;
    private Scene scene2;
    // Use this for initialization
    void Start()
    {
        if(SceneManager.sceneCount > 1)
        {
            SceneManager.LoadScene(0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene(1);
        }

    }
}
