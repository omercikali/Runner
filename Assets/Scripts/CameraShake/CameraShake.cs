using UnityEngine;
using DG.Tweening;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;

    private void Awake() => Instance = this;

    private void onShake(float duration, float strength){
        transform.DOShakePosition(duration,strength);
    }

    public static void shake(float duration, float strength) => Instance.onShake(duration,strength);
}
