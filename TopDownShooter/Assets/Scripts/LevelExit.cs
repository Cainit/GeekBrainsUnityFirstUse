using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LevelExit : MonoBehaviour
{
    void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("Level exit trigger!");
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #elif UNITY_WEBPLAYER
               Application.OpenURL(webplayerQuitURL);
            #else
               Application.Quit();
            #endif
        }
    }
}
