using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LevelExit : Trigger
{
    [SerializeField]
    string nextScene = "";

    void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(oneShot)
                GetComponent<Collider>().enabled = false;

            Debug.Log("Level exit trigger!");

            if (nextScene == "")
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
                Application.OpenURL(webplayerQuitURL);
#else
                Application.Quit();
#endif
            }
            else
            {
                GameManager.Instance.LoadScene(nextScene);
            }

        }
    }
}
