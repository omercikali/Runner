using UnityEngine;
using DG.Tweening;
using UnityEditor.Rendering.LookDev;
using UnityEngine.UI;
using PathCreation.Examples;

public class PlayerCollisions : MonoBehaviour
{
    [SerializeField] private GameObject bloodParticles;
    [SerializeField] private GameObject finishCollider;
    [SerializeField] private GameObject playerPos;
    [SerializeField] private GameObject pauseMenuButton;
    private Animator playerAnim;
    [SerializeField] private int coin;
    private Text CoinText;

    [SerializeField] private GameObject Player;

    public static bool gateBool;

    [SerializeField] private GameObject Image2x;

    private void Start()
    {
        CoinText = GameObject.Find("CoinText").GetComponent<Text>();
        //PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("Coin", 1000);
        Debug.Log("Coinplayerprefz" + PlayerPrefs.GetInt("Coin"));
        playerPos = GameObject.FindGameObjectWithTag("Player");
        gateBool = false;
        float repeatRate = 2f; // Tekrarlama süresi (saniye cinsinden)
        InvokeRepeating("getAnim", 0.0f, repeatRate);

        // PLATER X'İ SIFIRLAMA KONTROL KODU
        if (DOTween.IsTweening(transform) && DOTween.IsTweening(Image2x))    // DOTween'in yüklenip yüklenmediğini kontrol et
        {
            DOTween.Kill(transform); // Eğer zaten bir tween hareketi varsa durdur
            DOTween.Kill(Image2x);
        }

        PathFollower.speed = 3;
        Image2x.SetActive(false);
    }
    void getAnim()
    {
        if (playerAnim != null)
        {
            playerAnim.SetInteger("AttackIndex", Random.Range(0, 3));
        }
    }
    private void Awake()
    {
        playerAnim = GetComponent<Animator>();
        bloodParticles.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "checkbool")
        {
            Image2x.SetActive(true);
            PathFollower.speed = 2.3f;
            transform.DOMoveX(0f, 0.7f).SetEase(Ease.OutCubic);
            gateBool = true;
            pauseMenuButton.SetActive(false);
        }

        if (other.tag == "Size")
        {
            GameEvents.instance.playerSize.Value += 1;
            other.GetComponent<Collider>().enabled = false;
            other.transform.DOScale(Vector3.zero, 0.5f).OnComplete(() =>
            {
                Destroy(other.gameObject);
            });
        }
        if (other.tag == "Obstacle" && gateBool == false)
        {
            coin += 1;
            CoinText.text = "" + coin;
            other.GetComponent<Block>().CheckHit();
            CameraShake.shake(1f, 1f);
        }

        else if (other.tag == "Obstacle" && gateBool == true)
        {
            coin *= 2;
            other.GetComponent<Block>().finishExtra(coin);
        }

        if (other.tag == "tekme")
        {
            playerAnim.SetTrigger("kick1");
        }

        if (other.tag == "Gate")
            other.GetComponent<Gate>().ExecuteOperation();

        if (other.tag == "Saw")
        {
            GameEvents.instance.gameLost.SetValueAndForceNotify(true);
            bloodParticles.SetActive(true);
            GetComponent<Collider>().enabled = false;
        }

        if (other.tag == "Finish")
        {
            GameEvents.instance.gameWon.SetValueAndForceNotify(true);
        }
    }

}