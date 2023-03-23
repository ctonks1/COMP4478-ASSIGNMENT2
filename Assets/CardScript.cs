using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardScript : MonoBehaviour
{

    private bool facingUp = false;
    public string characterName = "";
    public Sprite characterSprite;
    public Sprite cardSprite;
    public Image image;
    public Button button;

    private bool locked = false;
    

    void Awake()
    {

    }


    // Start is called before the first frame update
    void Start()
    {
        image.sprite = cardSprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void flipCard()
    {
        //check if card is locked then flip the card
        if (!locked)
        {
            if (facingUp)
            {
                image.sprite = cardSprite;
            }
            else
            {
                image.sprite = characterSprite;
            }
            facingUp = !facingUp;
        }
    }

    //set the locked variable to true
    public void Lock()
    {
        locked = true;
    }
}
