using System.Collections;
using UnityEngine;
using DG.Tweening;

public class PanelTweenManager : MonoBehaviour
{
    [SerializeField] private GameObject panel;

    public void OpenPanel()
    {
        if (panel != null)
        {
            panel.SetActive(true);
            SettingsPanelTweenOn();
        }
    }

    public void ClosePanel()
    {
        if (panel != null)
        {
            SettingsPanelTweenOff();
            //StartCoroutine(WaitAndClosePanel(1f));
        }
    }

    void SettingsPanelTweenOn()
    {
        panel.transform.DOScale(new Vector3(0.95f, 0.7125f, 0.25f), 0.7f).SetEase(Ease.InExpo);
        // Vector3(0.863037229,0.717602074,0.559643626) 
    }

    void SettingsPanelTweenOff()
    {
        panel.transform.DOScale(Vector3.zero, 0.6f).SetEase(Ease.OutExpo);
    }


}
