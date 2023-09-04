using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> cards;
    [SerializeField] private int numberOfObjectsToSpawn;
    [SerializeField] private float distanceBetweenObjects;
    [SerializeField] private int objectsPerRow = 5;

    private void Start()
    {
        SpawnPrefabs();
    }

    private void SpawnPrefabs()
    {
        int numRows = Mathf.CeilToInt((float)numberOfObjectsToSpawn / objectsPerRow);
        int currentRow = 0;
        int currentColumn = 0;

        for (int i = 0; i < numberOfObjectsToSpawn; i++)
        {
            GameObject prefabToSpawn = cards[i % cards.Count];
            Vector3 spawnPosition = new Vector3(transform.position.x + currentColumn * distanceBetweenObjects, transform.position.y - currentRow * distanceBetweenObjects, transform.position.z );
            Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);

            currentColumn++;

            if (currentColumn >= objectsPerRow)
            {
                currentColumn = 0;
                currentRow++;
            }

            if (currentRow >= numRows && (i + 1) % objectsPerRow == 0)
            {
                currentRow = 0;
            }
        }
    }
}



