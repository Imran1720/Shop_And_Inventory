using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIUtility : MonoBehaviour
{
    public static UIUtility Instance;

    public List<Sprite> itemButtonRarityCart;

    private void Awake()
    {
        Instance = this;
    }


    public Sprite GetButtonRarity(Rarity _rarity)
    {
        return itemButtonRarityCart[(int)_rarity];
    }

}
