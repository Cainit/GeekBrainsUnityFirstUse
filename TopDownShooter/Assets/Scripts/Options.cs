using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Options : MonoBehaviour
{
    public static Options Instance;
    [SerializeField]
    UnityEngine.UI.Toggle fullscreenToogle;
    [SerializeField]
    TMP_Dropdown dropdownResolution;
    [SerializeField]
    TMP_Dropdown dropdownQuality;
    [SerializeField]
    UnityEngine.UI.Slider sliderAudio;

    Resolution[] resolutions;

    void Awake()
    {
        Instance = this;

        //create quality list
        dropdownQuality.ClearOptions();
        dropdownQuality.AddOptions(new List<string>(QualitySettings.names));

        //create resolution list
        dropdownResolution.ClearOptions();
        List<string> resNames = new List<string>();
        resolutions = Screen.resolutions;
        foreach (Resolution res in resolutions)
        {
            resNames.Add(res.width+"x"+res.height);
        }
        dropdownResolution.AddOptions(resNames);

        //close options
        Close();
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void Apply()
    {

        Close();
    }

    public void ResolutionChange(int index)
    {
        Screen.SetResolution(resolutions[index].width, resolutions[index].height, fullscreenToogle.isOn);
    }

    public void QualityChange(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }

    public void FullscreenToogle()
    {
        Screen.fullScreen = fullscreenToogle.isOn;
    }

    public void AudioChange()
    {

    }
}
