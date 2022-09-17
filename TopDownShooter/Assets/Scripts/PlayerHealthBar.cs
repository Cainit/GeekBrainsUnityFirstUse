using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    Image bar;

    void Start()
    {
        bar = transform.Find("Bar").GetComponent<Image>();
        PlayerController.Instance.GetComponent<Health>().OnChange += OnChange;
    }

    void OnChange(object sender, EventArgs args)
    {
        bar.transform.localScale = new Vector3(PlayerController.Instance.GetComponent<Health>().GetInPercent(), bar.transform.localScale.y, bar.transform.localScale.z);
    }
}
