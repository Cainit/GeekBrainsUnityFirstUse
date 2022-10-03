using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class PlayerGrenades : MonoBehaviour
{
    TMPro.TextMeshProUGUI bar;

    void Start()
    {
        bar = GetComponent<TextMeshProUGUI>();
        PlayerController.Instance.OnGrenades += OnChange;
    }

    void OnChange(object sender, EventArgs args)
    {
        bar.text = PlayerController.Instance.grenades.ToString();
    }
}
