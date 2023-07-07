using UnityEngine;
using DG.Tweening;
using UnityEditor.Rendering.LookDev;
using UnityEngine.UI;

public class PlayerCollisions : MonoBehaviour
{
    [SerializeField] private GameObject bloodParticles;
    [SerializeField] private GameObject finishCollider;
    [SerializeField] private GameObject playerPos;
    private Animator playerAnim;
    private int coin;
    private Text CoinText;

    public static bool gateBool;

    private void Start()
    {
        CoinText = GameObject.Find("CoinText").GetComponent<Text>();
        //PlayerPrefs.DeleteAll();
        Debug.Log("Coinplayerprefz" + PlayerPrefs.GetInt("Coin"));
        playerPos = GameObject.FindGameObjectWithTag("Player");
        gateBool = false;
        float repeatRate = 2f; // Tekrarlama süresi (saniye cinsinden)
        InvokeRepeating("getAnim", 0.0f, repeatRate);
        
    }
    void getAnim()
    {
        playerAnim.SetInteger("AttackIndex", Random.Range(0, 4));
    }
    private void Awake()
    {
        playerAnim = GetComponent<Animator>();
        bloodParticles.SetActive(false);
    }
    private void Update()
    {
       
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "checkbool")
        {
            gateBool = true;
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
            CoinText.text = "Coin: " + coin;
            other.GetComponent<Block>().CheckHit();
            CameraShake.shake(1f, 1f);
            Debug.Log("coin:"+coin);
        }

        else if(other.tag == "Obstacle" && gateBool == true)
        {
            coin *= 2;
            other.GetComponent<Block>().finishExtra(coin);
            Debug.Log("coin:" + coin);
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