using System.Collections;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using GoogleMobileAds.Api;

public class UIManager : MonoBehaviour
{
    private CompositeDisposable subscriptions = new CompositeDisposable();
    [SerializeField] private GameObject startUI;
    [SerializeField] private GameObject gameUI;
    [SerializeField] private GameObject winUI;
    [SerializeField] private GameObject loseUI;
    //reklamlar


    private InterstitialAd interstitialAd;
#if UNITY_ANDROID
    private string _adUnitId = "ca-app-pub-3940256099942544/8691691433";
#elif UNITY_IPHONE
  private string _adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
  private string _adUnitId = "unused";
#endif

    public void ShowAd()
    {
        if (interstitialAd != null && interstitialAd.CanShowAd())
        {
            Debug.Log("Showing interstitial ad.");
            interstitialAd.Show();
        }
        else
        {
            Debug.LogError("Interstitial ad is not ready yet.");
        }
    }
    public void LoadInterstitialAd()
    {

        // Clean up the old ad before loading a new one.
        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
            interstitialAd = null;
        }

        Debug.Log("Loading the interstitial ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest();
        adRequest.Keywords.Add("unity-admob-sample");

        // send the request to load the ad.
        InterstitialAd.Load(_adUnitId, adRequest,
            (InterstitialAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("interstitial ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("Interstitial ad loaded with response : "
                          + ad.GetResponseInfo());

                interstitialAd = ad;
            });
    }

    private void Start()
    {

        MobileAds.Initialize((InitializationStatus initStatus) =>
        {    
        });
        LoadInterstitialAd();

    }

 



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
                    ShowAd();



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