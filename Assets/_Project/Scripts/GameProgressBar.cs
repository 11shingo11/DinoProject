using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class GameProgressBar : MonoBehaviour
{
    [SerializeField] protected Slider Slider;
    [SerializeField] private TextMeshProUGUI barText;


    private float percentOfRemovedCards;
    private float valueOfProgressBar = 0;
    private float numsOfCards;


    private void Start()
    {
        Subscribe();
    }
    private void Subscribe()
    {
        GameManager.Instance.OnCardListChange += UpdateBar;
    }
    private void Unsubscribe()
    {
        GameManager.Instance.OnCardListChange -= UpdateBar;
    }

    private void UpdateBar()
    {
        if (percentOfRemovedCards == 0)
        {
            GetPercentOfRemovedCards();
        }
        int currentNumOfCards = GameManager.Instance.GetNuberOfCardsInGame() -2;
        Debug.Log(currentNumOfCards);
        ChangeBarText();
        OnValueChanged(currentNumOfCards, numsOfCards);
        
        if (valueOfProgressBar >= 100)
        { 
            Unsubscribe();
            Debug.Log("you win");
        }
        else
        {
            Unsubscribe();
            Subscribe();   
        }
             
    }

    private void ChangeBarText()
    {
        valueOfProgressBar += percentOfRemovedCards;
        barText.text = $"{Mathf.Ceil(valueOfProgressBar):0.##} %";
    }

    private void GetPercentOfRemovedCards()
    {
        numsOfCards = GameManager.Instance.GetNuberOfCardsInGame();//получаем количество карточек в игре
        percentOfRemovedCards = 200 / numsOfCards;
    }
    public void OnValueChanged(float value, float maxValue)
    {
        Slider.value = 1-(value / maxValue);
    }

}
