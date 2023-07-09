using UniRx;
using UnityEngine;
using System.Collections;
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
                if (value)
                    StartCoroutine(WaitAndActivate(winUI, 3f));
            })
            .AddTo(subscriptions);

        GameEvents.instance.gameLost.ObserveEveryValueChanged(x => x.Value)
            .Subscribe(value =>
            {
                if (value)
                    StartCoroutine(WaitAndActivate(loseUI, 3f));
                    
            })
            .AddTo(subscriptions);
    }

    private IEnumerator WaitAndActivate(GameObject _menu, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        ActivateMenu(_menu);
        // WIN VE LOSE UI ANIMATION
        _menu.transform.DOScale(new Vector3(0.6789026f, 0.6789026f, 0.6789026f), 0.7f)
            .SetEase(Ease.OutCubic);
    }

    private void OnDisable()
    {
        subscriptions.Clear();
    }

    private void ActivateMenu(GameObject _menu)
    {
        gameUI.SetActive(_menu == gameUI);
        startUI.SetActive(_menu == startUI);
        winUI.SetActive(_menu == winUI);
        loseUI.SetActive(_menu == loseUI);
    }

    //Level functions
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void NextLevel()
    {
        int newCurrentLevel = PlayerPrefs.GetInt("currentLevel", 1) + 1;
        int newLoadingLevel = PlayerPrefs.GetInt("loadingLevel", 1) + 1;

        if (newLoadingLevel >= SceneManager.sceneCountInBuildSettings)
            newLoadingLevel = 1;

        PlayerPrefs.SetInt("currentLevel", newCurrentLevel);
        PlayerPrefs.SetInt("loadingLevel", newLoadingLevel);

        SceneManager.LoadScene(newLoadingLevel);
    }
    public void HomeLevel()
    {
        SceneManager.LoadScene("Main");
    }


 
}