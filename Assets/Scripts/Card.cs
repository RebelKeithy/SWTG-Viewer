using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour {

    public CardData cardData;
    public GameObject model;

    private WWW www;
    private bool loadedTexture = false;

	// Use this for initialization
	void Start () {
        if(cardData.blocknumber == "1")
        {
            model.transform.localScale = new Vector3(1.4f, 1, 1);
        }

        www = new WWW(textureUrl());

	}
	
	// Update is called once per frame
	void Update () {
		if(!loadedTexture && www.isDone)
        {
            Renderer renderer = GetComponentInChildren<Renderer>();
            renderer.material.mainTexture = www.texture;
            loadedTexture = true;
        }
	}

    public void kill(Directions direction)
    {
        GetComponent<Animatable>().setTarget(transform.position + -10 * direction.vector, kill: true);
    }

    private string textureUrl()
    {
        Debug.Log("'" + cardData.furl + "'");
        if (cardData.furl != null && cardData.furl != "")
            return "http://www.cardgamedb.com/forums/uploads/sw/ffg_" + cardData.furl + ".png";
        else
            return "http://www.cardgamedb.com/forums/uploads/sw/ffg_" + cardData.img + ".png";
    }
}
