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

    private bool isNotificationActive = false;

    private void Start() => ToggleNotification(isNotificationActive);

    private void Update()
    {
        if (!isNotificationActive) return;

        timer -= Time.deltaTime;
        notificationSlider.value = timer;

        if (timer <= 0)
        {
            isNotificationActive = false;
            ToggleNotification(isNotificationActive);
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

    private void ToggleNotification(bool isEnabled)
    {
        notificationBackground.enabled = isEnabled;
        notificationSliderObject.SetActive(isEnabled);
        notificationTextObject.SetActive(isEnabled);
    }

    private void EnableNotification()
    {
        SoundManager.Instance.PlaySoundFX(Sounds.NOTIFICATION);
        ToggleNotification(true);
        ResetData();
    }
}
