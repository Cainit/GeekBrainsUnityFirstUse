using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorEmpty : MonoBehaviour
{
    [SerializeField]
    GameObject prefab;

    List<GameObject> objects = new List<GameObject>();

    void Start()
    {
        for(int i=0; i<1000; ++i)
        {
            objects.Add(Instantiate(prefab, transform.position, Quaternion.identity, transform));
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*
        foreach(GameObject obj in objects)
        {
            obj.transform.Rotate(Vector3.up);
        }
        */
    }
}
