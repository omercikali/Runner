using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelTweenManager : MonoBehaviour
{
    [SerializeField] private GameObject TweenPanelOn;
    [SerializeField] private GameObject TweenPanelOff;
    [SerializeField] private GameObject Panel;

    public void OpenPanel(){
        if(Panel != null){
            Panel.SetActive(true);
            SettingsPanelTweenOn();
        }
    }
    public void ClosePanel(){
        if(Panel != null){
            SettingsPanelTweenOff();
            WaitSecond(1f);
            if(!LeanTween.isTweening(TweenPanelOff)){
                Panel.SetActive(false);
            }
        }
    }

    void SettingsPanelTweenOn(){
        LeanTween.scale(TweenPanelOn,new Vector3(0.8630372f,0.7176021f,0.5596436f), 0.3f).setEaseInExpo();
        // Vector3(0.863037229,0.717602074,0.559643626) 
    }
    void SettingsPanelTweenOff(){
        LeanTween.scale(TweenPanelOff,new Vector3(0f,0f,0f), 0.2f).setEaseOutExpo();
    }

    IEnumerator WaitSecond(float wait){
        yield return new WaitForSeconds(wait);
    }
}   
