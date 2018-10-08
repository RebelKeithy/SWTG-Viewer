using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CardData : IComparable
{
    public string id;
    public string setid;
    public string number;
    public string name;

    // Organization
    public string fullset;
    public string side;
    public string affiliation;
    public string type;

    // Set Info
    public string setname;
    public string block;
    public string numericblock;
    public string blocknumber;

    // Icons
    public string unique;
    public string cost;
    public string textcost;
    public string force;
    public string fate;
    public string resources;
    public string health;

    // Combat
    public string tb;
    public string tw;
    public string bdb;
    public string bdw;
    public string udb;
    public string udw;

    // Text
    public string traits;
    public string text;
    public string flavor;

    public string fullurl;
    public string furl;
    public string img;

    public string illustrator;
    public string rating;
    public string blocklink;
    public string deck;
    public string label;
    public string numcomments;
    public string rabbr;

    public string getValue(string name)
    {
        if (name == "side")
            return side;
        if (name == "affiliation")
            return affiliation;
        return null;
    }

    public int CompareTo(object obj)
    {
        CardData card = obj as CardData;
        if(numericblock == card.numericblock)
        {
            return int.Parse(this.blocknumber) - int.Parse(card.blocknumber);
        } else
        {
            return int.Parse(this.numericblock) - int.Parse(card.numericblock);
        }
        
    }
}
