using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloatText : MonoBehaviour
{
    private TextMeshPro textmesh;
    public float fadeSpeed = 1f;
    public float timer;
    public Color colorOrigin;
    public Color colorAlpha = new Color(0,0,0,0);

    private void Awake()
    {
        textmesh = transform.GetComponent<TextMeshPro>();
        colorOrigin = textmesh.color;
    }

    public void Setup(float PointAmount)
    {
        textmesh.SetText(PointAmount.ToString());
    }

    private void Update()
    {
        float moveYSpeed = 15;
        transform.position += new Vector3(0, moveYSpeed) * Time.deltaTime;
        transform.rotation = Camera.main.transform.rotation;

        timer += 1.0f * Time.deltaTime * fadeSpeed;

        textmesh.color = Color.Lerp(colorOrigin, colorAlpha, timer);
    }
}