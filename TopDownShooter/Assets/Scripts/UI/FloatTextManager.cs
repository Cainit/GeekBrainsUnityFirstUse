using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloatTextManager : MonoBehaviour
{
    public static FloatTextManager Instance;
    public GameObject prefabDamage;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    public void ShowDamage(Vector3 position, float damage)
    {
        GameObject newFloatText = Instantiate(prefabDamage, position, Quaternion.identity, transform);
        newFloatText.GetComponent<TextMeshPro>().text = damage.ToString("F1");
        Destroy(newFloatText, 1f);
    }
}
