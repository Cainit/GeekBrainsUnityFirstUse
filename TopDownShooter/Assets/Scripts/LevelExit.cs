using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LevelExit : MonoBehaviour
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
