using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEnemy : MonoBehaviour
{
    public GameObject enemySpawnPrefab;
    public int maxSpawnCount = -1;
    public int currentSpawnCount = 0;

    GameObject currentSpawnInstance;

    void Awake()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }

    void Update()
    {
        if (currentSpawnInstance == null)
            SpawnUnit();

    }

    public void SpawnUnit()
    {
        if(maxSpawnCount > 0)
        {
            if(currentSpawnCount >= maxSpawnCount)
            {
                Destroy(this.gameObject);
                return;
            }
        }

        currentSpawnInstance = Instantiate(enemySpawnPrefab, transform.position, Quaternion.identity, transform.parent );
        currentSpawnInstance.tag = "Enemy";
        ++currentSpawnCount;
    }
}
