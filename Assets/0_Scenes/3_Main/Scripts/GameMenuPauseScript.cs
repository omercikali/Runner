using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenuPauseScript : MonoBehaviour
{
    [SerializeField] private GameObject menuObject;
    [SerializeField] private bool visibilityControl = false;

    private void Start()
    {
        menuObject.SetActive(false);
    }

    void Update()
    {
        if(menuObject.activeInHierarchy){
            visibilityControl = true;
        }
        else if(!menuObject.activeInHierarchy){
            visibilityControl = false;
        }

        if(visibilityControl == true){
            TimeScale0();
        }else{
            TimeScale1();
        }
    }

    private void TimeScale0(){
            Time.timeScale = 0;
    }
    private void TimeScale1(){
            Time.timeScale = 1;
    }
}
