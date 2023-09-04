using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private List<Card> cards = new List<Card>();

    private string firstCardName;
    private string secondCardName;
    private GameObject firstCard;
    private GameObject secondCard;
    private bool rotate = true;

    public event Action OnCardListChange;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }



    private void Start()
    {
        
        Card[]items = FindObjectsOfType<Card>();
        foreach (Card item in items)
            cards.Add(item);
        foreach (Card card in cards)
        {
            card.IsCardOpened += GetNamesOfOpendCards;
        }
    }


    private void GetNamesOfOpendCards(Card card)
    {
       
        if (firstCardName == null)
        {
            firstCard = card.gameObject;
            firstCardName = firstCard.name;
            rotate = false;
            //Debug.Log($"Get first Name{card.gameObject.name}");
            card.IsCardOpened -= GetNamesOfOpendCards;
        }
        else
        {
            secondCard = card.gameObject;
            secondCardName = secondCard.name;
            card.IsCardOpened -= GetNamesOfOpendCards;
            //Debug.Log($"Get second Name{card.gameObject.name}");

            StartCoroutine(CompareCardsNames());
            // Call the method to compare the two cards immediately after the second card is flipped.           
        }
        

    }

    private void Update()
    {
        if (firstCard != null)
        {
            CheckFirstCardState(firstCard.GetComponent<Card>());
        }
        
    }

    private IEnumerator CompareCardsNames()
    {
        yield return new WaitForSeconds(1.7f);
        //Debug.Log("Compare Name");
        if (firstCard.GetComponentInChildren<SpriteRenderer>().sprite.name == secondCard.GetComponentInChildren<SpriteRenderer>().sprite.name)
        {
            OnCardListChange?.Invoke();
            Debug.Log(firstCardName +" "+ secondCardName);
            cards.Remove(firstCard.GetComponent<Card>());
            cards.Remove(secondCard.GetComponent<Card>());
            //OnCardListChange?.Invoke();
            Destroy(firstCard);
            Destroy(secondCard);
            firstCardName = null;
            secondCardName = null;
            firstCard = null; // Обнулите ссылки на карты после их уничтожения
            secondCard = null;
            if (cards.Count == 0)
            {
                Debug.Log("you win!!!");
            }
        }
        else
        {
            StartCoroutine(ResetCards());
        }
    }

    private IEnumerator ResetCards()
    {
        yield return new WaitForSeconds(1f); // Delay before flipping the cards back.
        //Debug.Log("Reset Cards");
        // Reset the first card.
        Card firstCardComponent = firstCard.GetComponent<Card>();
        firstCardComponent.UnrotateCard();
        firstCardComponent.rotate = false;
        

        // Reset the second card.
        Card secondCardComponent = secondCard.GetComponent<Card>();
        secondCardComponent.UnrotateCard();
        secondCardComponent.rotate = false;

        // Clear the card references.
        firstCardName = null;
        secondCardName = null;
        firstCard = null; // Обнулите ссылки на карты после их уничтожения
        secondCard = null;
        foreach (Card card in cards)
        {
            card.IsCardOpened -= GetNamesOfOpendCards;
        }
        foreach (Card card in cards)
        {
            card.IsCardOpened += GetNamesOfOpendCards;
        }
    }

    private void CheckFirstCardState(Card firstCard)
    {
        if (!firstCard.rotate)
        {
            firstCard = null;
            firstCardName = null;
            rotate = false;
            foreach (Card card in cards)
            {
                card.IsCardOpened -= GetNamesOfOpendCards;
            }
            foreach (Card card in cards)
            {
                card.IsCardOpened += GetNamesOfOpendCards;
            }

        }
    }

    public int GetNuberOfCardsInGame()
    {
        int numberOfCards = cards.Count;
        //Debug.Log(numberOfCards);
        return numberOfCards;
    }



}
