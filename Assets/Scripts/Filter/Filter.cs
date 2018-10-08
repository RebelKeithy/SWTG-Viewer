using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Filter : MonoBehaviour {
    public CardManager cardManager;
    public string key;
    public string value;

    public void filter()
    {
        cardManager.filter(key, value);
    }

    public void filterSideLight()
    {
        cardManager.filter("side", "Light");
    }

    public void filterSideDark()
    {
        cardManager.filter("side", "Dark");
    }

    public void filterAffiliationJedi()
    {
        cardManager.filter("affiliation", "Jedi");
    }
}
