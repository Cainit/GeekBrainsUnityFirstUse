using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;
    
    void Awake()
    {
        Instance = this;
    }
    
    public static void PlayMusic()
    {

    }
}
