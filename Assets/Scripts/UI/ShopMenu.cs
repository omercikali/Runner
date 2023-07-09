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
    public Button buyButton;
    public Button pickButton;
    public Text coinText;
    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.DeleteAll();
        //PlayerPrefs.SetInt("Coin", 10000);
        // Manuel olarak modelprint dizisini doldurun

        currentIndex = PlayerPrefs.GetInt("SelectModel", 0);
        foreach (var model in models)
        {
            model.SetActive(false);
            models[currentIndex].SetActive(true);
        }
        LoadModelUnlockStatus();
    }
    // Update is called once per frame
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
            buyButton.gameObject.SetActive(false);
            pickButton.gameObject.SetActive(true);
        }
        else
        {
            buyButton.gameObject.SetActive(true);
            pickButton.gameObject.SetActive(false);
            buyButton.GetComponentInChildren<Text>().text = "Buy " + currentModel.price+"$";
            if (currentModel.price < PlayerPrefs.GetInt("Coin"))
            {
                buyButton.interactable = true;
            }
            else
            {
                buyButton.interactable = false;
            }
        }
    }
    public void BuyOrPickModel()
    {
        ModelPrint currentModel = modelprint[currentIndex];
        if (currentModel.isUnlocked)
        {
            // Pick up model
            PlayerPrefs.SetInt("SelectModel", currentIndex);
        }
        else
        {
            // Buy model
            if (currentModel.price <= PlayerPrefs.GetInt("Coin"))
            {
                PlayerPrefs.SetInt(currentModel.name, 1);
                PlayerPrefs.SetInt("SelectModel", currentIndex);
                currentModel.isUnlocked = true;
                PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin", 0) - currentModel.price);
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
    }
}
