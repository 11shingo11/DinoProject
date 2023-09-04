using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Card : MonoBehaviour
{
    public string objectName;
    private Animator animator;
    public bool rotate = false;

    public event Action<Card> IsCardOpened;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    

    public void OnMouseDown()
    {        
        //Debug.Log("MouseClicked");
        if (!rotate)
        {
            RotateCard();
        }
        else
        {
            UnrotateCard();
        }
        
    }

    public void RotateCard()
    {
        IsCardOpened?.Invoke(this);
        animator.SetTrigger("Clicked");
        rotate = true;
        objectName = gameObject.name;
        //Debug.Log(objectName);
    }

    public void UnrotateCard()
    {
        animator.SetTrigger("Unrotate");
        rotate = false;
    }

}
