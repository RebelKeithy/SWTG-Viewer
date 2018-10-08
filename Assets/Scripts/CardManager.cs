using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class CardManager : MonoBehaviour
{

    public GameObject cardPrefab;
    public TextAsset file_path;

    public int currCard = 0;
    public CardData[] cards;
    public List<GameObject> loaded_cards = new List<GameObject>();
    public List<CardData> filteredCards = new List<CardData>();
    public Dictionary<string, List<string>> filters = new Dictionary<string, List<string>>();

    private int cardsPerRow = 3;
    private int cardsPerCol = 2;

	// Use this for initialization
	void Start () {
        //Debug.Log(file_path.text);
        cards = JsonHelper.FromJson<CardData>("{\"Items\":" + file_path.text + "}");

        foreach(CardData card in cards)
        {
            if(card.numericblock != "" && card.blocknumber == "1")
                filteredCards.Add(card);
        }
        filteredCards.Sort();

        loadCard(currCard, Directions.none);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            nextCard();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            prevCard();
        }
    }

    public void prevCard()
    {
        if (currCard - (cardsPerCol * cardsPerRow) >= 0)
        {
            unloadCards(Directions.left);
            currCard -= cardsPerCol * cardsPerRow;
            loadCard(currCard, Directions.left);
        }
    }

    public void nextCard()
    {
        if (currCard + (cardsPerCol * cardsPerRow) < filteredCards.Count)
        {
            unloadCards(Directions.right);
            currCard += cardsPerCol * cardsPerRow;
            loadCard(currCard, Directions.right);
        }
    }

    public void unloadCards(Directions direction)
    {
        while (loaded_cards.Count > 0)
        {
            GameObject card = loaded_cards[0];
            card.GetComponent<Card>().kill(direction);
            //Destroy(card);
            loaded_cards.RemoveAt(0);
        }
    }

    public void loadCard(int id, Directions direction)
    {
        float rowSpacing = cardPrefab.transform.Find("Plane").GetComponent<Renderer>().bounds.size.x * 1.5f;
        float colSpacing = cardPrefab.transform.Find("Plane").GetComponent<Renderer>().bounds.size.y * 1.5f;
        Debug.Log("row: " + rowSpacing);
        for (int i = 0; i < cardsPerRow * cardsPerCol; i++)
        {
            if (i + id >= filteredCards.Count)
                break;
            float x = -(cardsPerRow * rowSpacing / 2f) + rowSpacing / 2f + (i % cardsPerRow) * rowSpacing;
            float y = (cardsPerCol * colSpacing / 2f) - colSpacing / 2f - (i / (cardsPerCol+1)) * colSpacing;
            Vector3 target = new Vector3(x, y, 0);
            Vector3 start = target + 10 * direction.vector;
            GameObject card = Instantiate(cardPrefab, start, Quaternion.identity);
            card.GetComponent<Animatable>().setTarget(target);
            card.GetComponent<Card>().cardData = filteredCards[id + i];
            loaded_cards.Add(card);
        }
    }

    public void filter(string key, string value)
    {
        bool found = false;
        foreach(string filter in filters.Keys)
        {
            if(filter == key)
            {
                found = true;
                List<string> values = filters[filter];
                if(values.Contains(value))
                {
                    values.Remove(value);
                    if(values.Count == 0)
                    {
                        filters.Remove(filter);
                        continue;
                    }
                }
                else
                {
                    values.Add(value);
                }
            }
        }

        if(!found)
        {
            List<string> values = new List<string> { value };
            filters.Add(key, values);
        }
        updateFilter();
    }

    public void updateFilter()
    {
        filteredCards.Clear();
        foreach (CardData card in cards)
        {
            bool shouldAdd = true;
            if (card.numericblock != "" && card.blocknumber == "1")
            {
                foreach(string filter in filters.Keys)
                {
                    bool anyMatch = false;
                    foreach(string value in filters[filter])
                    {
                        Debug.Log(filter + " " + value + " " + card.getValue(filter));
                        if (card.getValue(filter) == value)
                            anyMatch = true;
                    }
                    if(!anyMatch)
                    {
                        shouldAdd = false;
                        break;
                    }
                }
            }
            else
            {
                shouldAdd = false;
            }

            if (shouldAdd)
            {
                filteredCards.Add(card);
            }
        }
        
        unloadCards(Directions.none);
        currCard = 0;
        loadCard(currCard, Directions.none);
    }
}
