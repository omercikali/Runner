using UnityEngine;

public class CameraHolder : MonoBehaviour
{
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;
    [SerializeField] private GameObject player3;
    public int currentIndex = 0;
    private Vector3 initRotation;


    private void Awake()
    {
        initRotation = transform.eulerAngles;
    }

    private void Update()
    {

            if (player1.activeInHierarchy)
            {
                transform.position = new Vector3(player1.transform.position.x, player1.transform.position.y, player1.transform.position.z);
                transform.eulerAngles = new Vector3(player1.transform.eulerAngles.x + initRotation.x, player1.transform.eulerAngles.y + initRotation.y, 0);
            }
             else if (player2.activeInHierarchy) 
             {
                transform.position = new Vector3(player2.transform.position.x, player2.transform.position.y, player2.transform.position.z);
                transform.eulerAngles = new Vector3(player2.transform.eulerAngles.x + initRotation.x, player2.transform.eulerAngles.y + initRotation.y, 0);

             }
            else if (player3.activeInHierarchy)
            {
                transform.position = new Vector3(player3.transform.position.x, player3.transform.position.y, player3.transform.position.z);
                transform.eulerAngles = new Vector3(player3.transform.eulerAngles.x + initRotation.x, player3.transform.eulerAngles.y + initRotation.y, 0);

            }


        if (GameEvents.instance.gameWon.Value || GameEvents.instance.gameLost.Value) return;
    }
}