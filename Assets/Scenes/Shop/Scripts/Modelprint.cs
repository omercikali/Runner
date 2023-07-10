using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public bool isSelected;

    public ModelPrint(string name, int price, bool isUnlocked, bool isSelected)
    {
        this.name = name;
        this.price = price;
        this.isUnlocked = isUnlocked;
        this.isSelected = isSelected;
    }

}
