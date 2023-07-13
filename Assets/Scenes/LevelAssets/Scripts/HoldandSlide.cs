using DG.Tweening;
using UnityEngine;

public class HoldandSlide : MonoBehaviour
{
    private void Start()
    {
        ScaleUI();
    }
    private void ScaleUI()
    {
        transform.DOScale(new Vector3(5.5f,0.9f,0.9f), 1f).SetLoops(25, LoopType.Yoyo);
    }
}