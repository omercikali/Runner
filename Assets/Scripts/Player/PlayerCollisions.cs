using UnityEngine;
using DG.Tweening;

public class PlayerCollisions : MonoBehaviour
{
    [SerializeField] private GameObject bloodParticles;
    [SerializeField] private GameObject finishCollider;
    [SerializeField] private GameObject playerPos;
    private Animator playerAnim;
    private int coin;
   
    public static bool gateBool;

    private void Start()
    {
        //PlayerPrefs.DeleteAll();
        Debug.Log("Coinplayerprefz" + PlayerPrefs.GetInt("Coin"));
        playerPos = GameObject.FindGameObjectWithTag("Player");
        gateBool = false;
    }
    private void Awake()
    {
        playerAnim = GetComponent<Animator>();
        bloodParticles.SetActive(false);
    }
    private void Update()
    {
        playerAnim.SetInteger("AttackIndex", Random.Range(0, 4));
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
            other.GetComponent<Block>().CheckHit();
            CameraShake.shake(1f, 1f);
            Debug.Log("coin:"+coin);
        }

        else if(other.tag == "Obstacle" && gateBool == true)
        {
            coin *= 2;
            other.GetComponent<Block>().finish11(coin);
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