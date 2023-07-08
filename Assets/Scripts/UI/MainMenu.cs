using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private Text coinText;
    [SerializeField]
    private Text diamondText;

    // Start is called before the first frame update
    void Start()
    {
        coinText.text =""+PlayerPrefs.GetInt("Coin");
        diamondText.text="55"+ PlayerPrefs.GetInt("Diamond");

    }

    // Update is called once per frame
    void Update()
    {
      
    }
}
