using UnityEngine;

public class EnergyTweenRotate : MonoBehaviour
{
    [SerializeField] private Vector3 rotateVector = new Vector3(0, 5f, 0);
    [SerializeField] private float speed = 10;
    void Update()
    {
        transform.Rotate(rotateVector * speed * Time.deltaTime);
    }

}
