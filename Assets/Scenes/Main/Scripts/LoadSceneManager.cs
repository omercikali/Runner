using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour
{
	public void ChangeScene()
	{
		int currentIndex = SceneManager.GetActiveScene().buildIndex;

		if (PlayerPrefs.GetInt("Levels")>1 ) 
		{
            SceneManager.LoadScene(PlayerPrefs.GetInt("Levels"));
        }
        else SceneManager.LoadScene(2);

    }
}
