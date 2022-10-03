using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInventory : MonoBehaviour
{
    static public KeyInventory Instance;

    void Awake()
    {
        Instance = this;
    }

    public void AddKey(GameObject prefab, int keyID)
    {
        GameObject newKey = Instantiate(prefab, this.transform);
        newKey.name = keyID.ToString();
    }

    
}
