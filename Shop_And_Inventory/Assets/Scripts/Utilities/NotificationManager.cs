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

    [SerializeField] private Color defaultColor;
    [SerializeField] private Color warningColor;

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
            notificationText.color = defaultColor;
            HideNotification();
        }
    }

    private void HideNotification() => gameObject.SetActive(false);
    public void SetNotificationData(string _text)
    {
        notificationText.text = _text;
        ResetTimer();
    }
    public void SetWarningNotificationData(string _text)
    {
        notificationText.color = warningColor;
        notificationText.text = _text;
        ResetTimer();
    }

    private void ResetTimer()
    {
        timer = notificationDuration;
        slider.value = timer;
    }
}
