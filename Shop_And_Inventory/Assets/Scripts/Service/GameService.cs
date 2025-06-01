using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameService : MonoBehaviour
{
    public static GameService instance;

    [SerializeField] private UIManager uiManager;
    public UIManager UIManager => uiManager;

    [SerializeField] List<Sprite> itemButtonRarityCart;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }


    public Sprite GetButtonRarity(Rarity _rarity)
    {
        return itemButtonRarityCart[(int)_rarity];
    }
}
