using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopMenu : MonoBehaviour
{
    public ModelPrint[] modelprint;
    public GameObject[] models;
    public int currentIndex = 0;
    public Button pickAndBuyButton;
    public Text coinText;
    public Text playerNameTXT;

    private const string SelectedPlayerPrefsKey = "SelectedPlayer";

    void Start()
    {
         //PlayerPrefs.DeleteAll();
        // PlayerPrefs.SetInt("Coin", 10000);
        // Manuel olarak modelprint dizisini doldurun

        currentIndex = PlayerPrefs.GetInt("SelectModel", 0);

        foreach (var model in models)
        {
            model.SetActive(false);
            models[currentIndex].SetActive(true);
        }
        LoadModelUnlockStatus();
        UpdateUI();
    }

    void Update()
    {
        coinText.text = "" + PlayerPrefs.GetInt("Coin");
        UpdateUI();
    }

    public void NextModel()
    {
        models[currentIndex].SetActive(false);
        currentIndex++;
        if (currentIndex == models.Length)
        {
            currentIndex = 0;
        }
        models[currentIndex].SetActive(true);
        PlayerPrefs.SetInt("SelectModel", currentIndex);
        UpdateUI();
    }

    public void BackModel()
    {
        models[currentIndex].SetActive(false);
        currentIndex--;
        if (currentIndex == -1)
        {
            currentIndex = models.Length - 1;
        }
        models[currentIndex].SetActive(true);
        PlayerPrefs.SetInt("SelectModel", currentIndex);
        UpdateUI();
    }

    public void Back()
    {
        SceneManager.LoadScene(0);
    }

    void UpdateUI()
    {
        ModelPrint currentModel = modelprint[currentIndex];

        if (currentModel.isUnlocked)
        {
            if (currentModel.isSelected)
            {
                pickAndBuyButton.GetComponentInChildren<Text>().text = "Selected";
                pickAndBuyButton.interactable = false;
            }
            else
            {
                pickAndBuyButton.GetComponentInChildren<Text>().text = "Pick Up";
                pickAndBuyButton.interactable = true;
            }
        }
        else
        {
            if (currentModel.price == 0)
            {
                pickAndBuyButton.GetComponentInChildren<Text>().text = "Free";
            }
            else
            {
                pickAndBuyButton.GetComponentInChildren<Text>().text = "Buy " + currentModel.price + " G";

                if (currentModel.price < PlayerPrefs.GetInt("Coin"))
                {
                    pickAndBuyButton.interactable = true;
                }
                else
                {
                    pickAndBuyButton.interactable = false;
                }
            }
        }

        playerNameTXT.text = currentModel.name;
    }

    public void BuyOrPickModel()
    {
        ModelPrint currentModel = modelprint[currentIndex];

        if (currentModel.isUnlocked && !currentModel.isSelected)
        {
            // Pick up model
            PlayerPrefs.SetInt("SelectModel", currentIndex);
            currentModel.isSelected = true;

            // Diðer karakterlerin isSelected deðerini false yap
            for (int i = 0; i < modelprint.Length; i++)
            {
                if (i != currentIndex)
                {
                    modelprint[i].isSelected = false;
                }
            }

            PlayerPrefs.SetInt(SelectedPlayerPrefsKey, currentIndex); // Seçili player'ý PlayerPrefs ile kaydet
            UpdateUI();
        }
        else if (!currentModel.isUnlocked)
        {
            // Buy model
            if (currentModel.price <= PlayerPrefs.GetInt("Coin"))
            {
                PlayerPrefs.SetInt(currentModel.name, 1);
                PlayerPrefs.SetInt("SelectModel", currentIndex);
                currentModel.isUnlocked = true;
                PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin", 0) - currentModel.price);
                UpdateUI();
            }
        }
    }

    void LoadModelUnlockStatus()
    {
        foreach (var model in modelprint)
        {
            int isUnlocked = PlayerPrefs.GetInt(model.name, 0);
            model.isUnlocked = isUnlocked == 1 ? true : false;
        }

        // Seçili player'ý yükle
        int selectedPlayerIndex = PlayerPrefs.GetInt(SelectedPlayerPrefsKey, 0);
        modelprint[selectedPlayerIndex].isSelected = true;
    }
}

