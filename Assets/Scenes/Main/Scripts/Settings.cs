using UnityEngine;
    
public class Settings : MonoBehaviour
{
    // --------------------------------------------------------
    [Header("Right Panel")]
    [SerializeField] private GameObject rightPanel;
    // --------------------------------------------------------
    // --------------------------------------------------------
    [Header("Settings Panel")]
    [SerializeField] private GameObject settingsButton;
    // --------------------------------------------------------
    // --------------------------------------------------------
    [Header("WatchAD Panel")]
    [SerializeField] private GameObject watchAdButton;
    // --------------------------------------------------------
    // --------------------------------------------------------
    [Header("RemoveAD Panel")]
    [SerializeField] private GameObject removeAdButton;
    // --------------------------------------------------------

    void Start()
    {
        // PANELS IS PASSIVE
        rightPanel.SetActive(true);
        settingsButton.SetActive(false);
        watchAdButton.SetActive(false);
        removeAdButton.SetActive(false);
    }
    
}

