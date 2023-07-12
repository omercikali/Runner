using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;

public class PlayerHolder : MonoBehaviour
{
    public GameObject[] models;
    // Start is called before the first frame update
    void Start()
    {
    //    PlayerPrefs.DeleteKey("SelectModel");

        foreach (var model in models)
        {
            model.SetActive(false);

        }
           models[PlayerPrefs.GetInt("SelectModel")].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
