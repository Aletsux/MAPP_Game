using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableCost : MonoBehaviour
{

    public int indexCost;
    public int cost;

    //public int costVaiable = List<int>planetCosts[index]
    // Start is called before the first frame update
    void Start()
    {
        StoreScript planetManager = FindObjectOfType<StoreScript>();
        if (planetManager != null && planetManager.planetCosts.Count > 0)
        {
            // or any other index you want to use
            cost = planetManager.planetCosts[indexCost];
            // use the cost variable for localization or other purposes
        }


    }
}
