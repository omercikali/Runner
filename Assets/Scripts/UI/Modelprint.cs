using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ModelPrint
{
    //public string name;
    //public int index;
    //public int price;
    //public bool isUnlocked;

    public string name;
    public int price;
    public bool isUnlocked;

    public ModelPrint(string name, int price, bool isUnlocked)
    {
        this.name = name;
        this.price = price;
        this.isUnlocked = isUnlocked;
    }

}
