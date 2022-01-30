    using System;
    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notifications : MonoBehaviour
{
    private CanvasGroup _group;
    private float _notificationTimeToLive;
    public AudioSource notificationSound;

    // Start is called before the first frame update
    void Start()
    {
        _group = gameObject.GetComponentInChildren<CanvasGroup>();
        _group.alpha = 0;
    }

    private void OnEnable()
    {
        EventManager.StartListening("NewTextMessage", ShowNotification);
    }

    private void OnDisable()
    {
        EventManager.StopListening("NewTextMessage", ShowNotification);
    }

    private void Update()
    {
        if (_notificationTimeToLive > 0)
        {
            _notificationTimeToLive -= Time.deltaTime;
        }
        else
        {
            _notificationTimeToLive = 0;
            _group.alpha = 0;
        }
    }

    private void ShowNotification()
    {
        _group.alpha = 1;
        _notificationTimeToLive += 3f;
        notificationSound.Play();
    }
}
