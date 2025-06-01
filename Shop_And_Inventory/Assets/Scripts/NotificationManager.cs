using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NotificationManager : MonoBehaviour
{
    public static NotificationManager Instance;
    [SerializeField] private float notificationDuration;
    [SerializeField] private TextMeshProUGUI notificationText;
    [SerializeField] private Slider slider;


    private float timer;


    private void Awake() => Instance = this;

    void Start() => InitializeData();

    private void InitializeData()
    {
        timer = notificationDuration;
        slider.maxValue = notificationDuration;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        slider.value = timer;
        if (timer <= 0)
        {
            HideNotification();
        }
    }

    private void HideNotification() => gameObject.SetActive(false);
    public void SetNotificationData(string itemName)
    {
        notificationText.text = "You Bought a " + itemName;
        ResetTimer();
    }

    private void ResetTimer()
    {
        timer = notificationDuration;
        slider.value = timer;
    }
}
