using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using TMPro;
using System.Globalization;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class CreateCards : MonoBehaviour
{
    public Button cardPrefab;

    public Transform gameLayout;

    public Sprite cardImage;
    public Sprite minion;
    public Sprite bean;
    public Sprite doraemon;
    public Sprite mouse;
    public Sprite noddy;
    public Sprite popeye;
    public Sprite scooby;
    public Sprite shincan;

    public GameObject gameOverScreen;
    public TMP_Text gameOverText;
    public Button restartButton;

    private List<Sprite> options = new List<Sprite>();

    private List<Button> cards = new List<Button>();
    private List<Button> playingCards = new List<Button>();

    private List<Button> selectedCards = new List<Button>();

    private int score = 0;


    // Start is called before the first frame update
    void Start()
    {
        //Hide the game over screen
        restartButton.onClick.AddListener(restartGame);
        restartButton.gameObject.SetActive(false);
        gameOverScreen.SetActive(false);
        gameOverText.enabled = false;

        //Initialize the cards
        loadOptionsArray();
        

        //Add the cards to the board
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                System.Random random = new System.Random();
                int randomNumber = random.Next(0, cards.Count);
                Button card = cards[randomNumber];
                cards.RemoveAt(randomNumber);

                card.onClick.AddListener(() => StartCoroutine(flipCard(card)));

                card.name = "card" + i + "_" + i;
                card.transform.SetParent(gameLayout, false);

                playingCards.Add(card);
            }            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public IEnumerator flipCard(Button card)
    {
        //Check if same card is selected
        if (!selectedCards.Contains(card))
        {
            //flip the card
            selectedCards.Add(card);
            CardScript cardScript = card.GetComponent<CardScript>();
            cardScript.flipCard();
        }
        
        
        //check if theres 2 cards selected
        if (selectedCards.Count == 2) 
        {
            //check if the cards match and then lock them and increase the score if they do
            CardScript card1Script = selectedCards[0].GetComponent<CardScript>();
            CardScript card2Script = selectedCards[1].GetComponent<CardScript>();
            if (card1Script.characterName == card2Script.characterName)
            {
                card1Script.Lock();
                card2Script.Lock();
                score++;
                if (score == 8)
                {
                    endGame();
                }
                Debug.Log(score);
            }
            selectedCards = new List<Button>();
            yield return new WaitForSeconds(0.5f);
            card1Script.flipCard();
            card2Script.flipCard();
            
        }
        
        yield return new WaitForSeconds(0);
    }

    private void restartGame()
    {
        //Restart the scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }

    private void endGame()
    {
        //show the game over screen
        restartButton.gameObject.SetActive(true);
        gameOverScreen.SetActive(true);
        gameOverText.enabled = true;
    }

    void loadOptionsArray()
    {
        //create each card
        Button card;
        Button card2;
        card = createCard("popeye", popeye);
        card2 = createCard("popeye", popeye);
        cards.Add(card);
        cards.Add(card2);

        card = createCard("minion", minion);
        card2 = createCard("minion", minion);
        cards.Add(card);
        cards.Add(card2);

        card = createCard("bean", bean);
        card2 = createCard("bean", bean);
        cards.Add(card);
        cards.Add(card2);

        card = createCard("mouse", mouse);
        card2 = createCard("mouse", mouse);
        cards.Add(card);
        cards.Add(card2);

        card = createCard("noddy", noddy);
        card2 = createCard("noddy", noddy);
        cards.Add(card);
        cards.Add(card2);

        card = createCard("scooby", scooby);
        card2 = createCard("scooby", scooby);
        cards.Add(card);
        cards.Add(card2);

        card = createCard("shincan", shincan);
        card2 = createCard("shincan", shincan);
        cards.Add(card);
        cards.Add(card2);

        card = createCard("doraemon", doraemon);
        card2 = createCard("doraemon", doraemon);
        cards.Add(card);
        cards.Add(card2);

    }

    Button createCard(string name, Sprite characterSprite)
    {
        //initialize a card button
        Button card;

        CardScript cardScript;
        card = Instantiate(cardPrefab);
        cardScript = card.GetComponent<CardScript>();
        cardScript.characterName = name;
        cardScript.cardSprite = cardImage;
        cardScript.characterSprite = characterSprite;
        return card;
    }
}
