using UnityEngine;
using DG.Tweening;

public class BounceTween : MonoBehaviour
{
    void Start()
    {
        PlayContinuousAnimation();
    }
    private void PlayContinuousAnimation()
    {
        Vector3 targetScale = new Vector3(transform.localScale.x + 0.02f, transform.localScale.y + 0.02f, transform.localScale.z + 0.02f);
        transform.DOScale(targetScale, 1f)
            .SetEase(Ease.InExpo)
            .SetLoops(-1, LoopType.Yoyo);
    }
}
