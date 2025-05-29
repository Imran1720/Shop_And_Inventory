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


    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        timer = notificationDuration;
        slider.maxValue = notificationDuration;
    }

    private void OnEnable()
    {
        //EventService.Instance.OnItemBought.AddListener(ShowNotification);
    }
    private void OnDisable()
    {
        //EventService.Instance.OnItemBought.RemoveListener(ShowNotification);
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        slider.value = timer;
        if (timer <= 0)
        {
            HideNotification();
        }
    }

    private void HideNotification()
    {
        gameObject.SetActive(false);
    }

    private void ShowNotification(ItemData data)
    {
        SetNotificationData(data.itemName);
        gameObject.SetActive(true);
    }

    public void SetNotificationData(string itemName)
    {
        notificationText.text = "You Bought a " + itemName;
        timer = notificationDuration;
    }

}
