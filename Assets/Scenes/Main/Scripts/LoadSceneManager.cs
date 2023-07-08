using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour
{
   

    private void Start()
    {
       // PlayerPrefs.DeleteKey("Levels");

    }
    public void ChangeScene()
    {
        Debug.Log("aa" + PlayerPrefs.GetInt("Levels"));
       
        if (PlayerPrefs.GetInt("Levels") == 0)
        {
            SceneManager.LoadScene(1);


        }
        else
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt("Levels"));
        }
    }
}
