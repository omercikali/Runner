﻿using UniRx;
using UnityEngine;
using UniRx.Triggers;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    private CompositeDisposable subscriptions = new CompositeDisposable();

    [SerializeField] private float limitX;
    [SerializeField] private float sidewaySpeed;
    [SerializeField] private Transform playerModel;

    // PLAYER ROTATE
    public float rotationSpeed = 2f; // Dönüş hızı
    private Quaternion targetRotation; // Hedef dönüş rotasyonu

    // Global Değişken
    private float eksiBir;
    private float artiBir;

    private bool lockControls;
    private float _finalPos;
    private float _currentPos;

    private void Start()
    {
        eksiBir = -1f;
        artiBir = 1f;
    }

private void OnEnable()
    {
        StartCoroutine(Subscribe());
    }
    private IEnumerator Subscribe()
    {
        yield return new WaitUntil(() => GameEvents.instance != null);
        this.UpdateAsObservable()
            .Where(_ => Input.GetMouseButton(0))
            .Subscribe(x =>
            {
                if (GameEvents.instance.gameStarted.Value && !GameEvents.instance.gameLost.Value
                && !GameEvents.instance.gameWon.Value)
                {
                    MovePlayer();
                }
            })
            .AddTo(subscriptions);

        GameEvents.instance.gameWon.ObserveEveryValueChanged(x => x.Value)
            .Subscribe(value =>
            {
                if (value)
                    lockControls = true;
            })
            .AddTo(subscriptions);

        GameEvents.instance.gameLost.ObserveEveryValueChanged(x => x.Value)
            .Subscribe(value =>
            {
                if (value)
                    lockControls = true;
            })
            .AddTo(subscriptions);
    }
    private void OnDisable()
    {
        subscriptions.Clear();
    }

    private void MovePlayer()
    {
        if (Input.GetMouseButton(0))
        {
            if (PlayerCollisions.gateBool == true)
            {
                eksiBir = 0f;
                artiBir = 0f;
            }
            float percentageX = (Input.mousePosition.x - Screen.width / 2) / (Screen.width * 0.5f) * 2;
            percentageX = Mathf.Clamp(percentageX, eksiBir, artiBir);
            _finalPos = percentageX * limitX;
        }

        float delta = _finalPos - _currentPos;
        _currentPos += (delta * Time.deltaTime * sidewaySpeed);
        _currentPos = Mathf.Clamp(_currentPos, -limitX, limitX);
        playerModel.localPosition = new Vector3(0, _currentPos, 0);

        // Hedef rotasyonu hesapla
        targetRotation = Quaternion.Euler(0, Mathf.Sign(delta) * 20, 0);

        // Karakterin rotasyonunu yavaşça hedef rotasyona doğru interpolate et
        playerModel.rotation = Quaternion.Lerp(playerModel.rotation, targetRotation, rotationSpeed * Time.deltaTime);

    }
}