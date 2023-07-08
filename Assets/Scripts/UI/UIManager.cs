using System.Collections;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    private CompositeDisposable subscriptions = new CompositeDisposable();
    [SerializeField] private GameObject startUI;
    [SerializeField] private GameObject gameUI;
    [SerializeField] private GameObject winUI;
    [SerializeField] private GameObject loseUI;

    private void OnEnable()
    {
        StartCoroutine(Subscribe());
        gameUI.SetActive(true);
        startUI.SetActive(true);
    }
    private IEnumerator Subscribe()
    {
        yield return new WaitUntil(() => GameEvents.instance != null);

        GameEvents.instance.gameStarted.ObserveEveryValueChanged(x => x.Value)
            .Subscribe(value =>
            {
                if (value)
                    ActivateMenu(gameUI);
            })
            .AddTo(subscriptions);

        GameEvents.instance.gameWon.ObserveEveryValueChanged(x => x.Value)
            .Subscribe(value =>
            {
                // WIN UI
                if (value)
                    StartCoroutine(WaitWinUI());
                    IEnumerator WaitWinUI()
                    {
                        yield return new WaitForSeconds(2);
                        ActivateMenu(winUI);
                         winUI.transform.DOScale(new Vector3(.67f, .67f, .67f), 1f).SetEase(Ease.OutElastic);
                         // REKLAM EKLENECEK

                     }
            })
            .AddTo(subscriptions);

        GameEvents.instance.gameLost.ObserveEveryValueChanged(x => x.Value)
            .Subscribe(value =>
            {
                if (value)
                    StartCoroutine(WaitLouseUI());
                    IEnumerator WaitLouseUI()
                    {
                        yield return new WaitForSeconds(2);
                        ActivateMenu(loseUI);
                        loseUI.transform.DOScale(new Vector3(.67f, .67f, .67f), 1f).SetEase(Ease.OutElastic);
                    }
            })
            .AddTo(subscriptions);
    }
    
    private void OnDisable()
    {
        subscriptions.Clear();
    }

    private void ActivateMenu(GameObject _menu)
    {
        gameUI.SetActive(false);
        startUI.SetActive(false);
        winUI.SetActive(false);
        loseUI.SetActive(false);

        _menu.SetActive(true);
    }

    //Level functions
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void NextLevel()
    {
        //int newCurrentLevel = PlayerPrefs.GetInt("currentLevel", 1) + 1;
        //int newLoadingLevel = PlayerPrefs.GetInt("loadingLevel", 1) + 1;

        //if (newLoadingLevel >= SceneManager.sceneCountInBuildSettings)
        //    newLoadingLevel = 1;

        //PlayerPrefs.SetInt("currentLevel", newCurrentLevel);
        //PlayerPrefs.SetInt("loadingLevel", newLoadingLevel);

        //SceneManager.LoadScene(newLoadingLevel);
      
            int activeScene = SceneManager.GetActiveScene().buildIndex;
          if (activeScene >= PlayerPrefs.GetInt("Levels"))
        {
            PlayerPrefs.SetInt("Levels", activeScene + 1);
            SceneManager.LoadScene(activeScene + 1); ;



        }
        else
        {
            SceneManager.LoadScene(activeScene + 1); ;

        }
    }
    public void HomeLevel()
    {
        SceneManager.LoadScene("Main");
    }

}