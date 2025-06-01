using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NotificationUIManager : MonoBehaviour, INotificationManager
{
    [Header("GameObject")]
    [SerializeField] private GameObject notificationTextObject;
    [SerializeField] private GameObject notificationSliderObject;

    [Header("Text")]
    [SerializeField] private TextMeshProUGUI notificationText;

    [Header("Slider")]
    [SerializeField] private Slider notificationSlider;

    [Header("Image")]
    [SerializeField] private Image notificationBackground;

    private float duration = 3f;
    private float timer;

    private string outOfFundMessage;
    private string inventoryFullMessage;

    private bool isNotificationActive;

    private void Start()
    {
        HideNotificationUI();
    }
    private void Update()
    {
        if (!isNotificationActive) return;

        timer -= Time.deltaTime;
        notificationSlider.value = timer;

        if (timer <= 0)
        {
            isNotificationActive = false;
            HideNotificationUI();
        }
    }

    public void SetData(float _value, string _outOfFundMessage, string _InventoryFullMessage)
    {
        duration = _value;
        inventoryFullMessage = _InventoryFullMessage;
        outOfFundMessage = _outOfFundMessage;
        notificationSlider.maxValue = duration;
    }

    private void ResetData()
    {
        timer = duration;
        isNotificationActive = true;
    }

    public void ShowNotification(string message)
    {
        EnableNotification();
        notificationText.text = message;
    }


    public void ShowInventoryFull()
    {
        EnableNotification();
        notificationText.text = inventoryFullMessage;
    }

    public void ShowOutOfFund()
    {
        EnableNotification();
        notificationText.text = outOfFundMessage;
    }

    private void ShowNotificationUI()
    {
        notificationBackground.enabled = true;
        notificationSliderObject.SetActive(true);
        notificationTextObject.SetActive(true);
    }

    private void HideNotificationUI()
    {
        notificationBackground.enabled = false;
        notificationSliderObject.SetActive(false);
        notificationTextObject.SetActive(false);
    }
    private void EnableNotification()
    {
        SoundManager.Instance.PlaySoundFX(Sounds.NOTIFICATION);
        ShowNotificationUI();
        ResetData();
    }
}
