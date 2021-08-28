using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemProperty : MonoBehaviour
{
    public string name;

    public int cost;


    public string GetName()
    {
        return name;
    }

    public int GetCost()
    {
        return cost;
    }
}
