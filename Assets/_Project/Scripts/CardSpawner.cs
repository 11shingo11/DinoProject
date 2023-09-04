using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CardSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> cards;
    [SerializeField] private int numberOfObjectsToSpawn;
    [SerializeField] private float distanceBetweenObjects;
    [SerializeField] private int objectsPerRow = 5;

    private void Start()
    {
        SpawnPrefabs();
        GameManager.Instance.OnCardsListCreate += ShuffleCards;
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

    private void ShuffleCards()
    {
        var cards = GameManager.Instance.Cards;


        List<Vector3> cardsPosition = new List<Vector3>();
        foreach (var card in cards)
        {
            cardsPosition.Add(card.gameObject.transform.position);

        }

        for (int i=0; i<cards.Count;i++)
        {
            int randIndex = Random.Range(0, cardsPosition.Count);
            
            var position = cardsPosition[randIndex];
            cards[i].gameObject.transform.position = position ;
            cardsPosition.RemoveAt(randIndex);

        }
        GameManager.Instance.OnCardsListCreate -= ShuffleCards;
    }
}




