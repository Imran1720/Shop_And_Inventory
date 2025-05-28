using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopView : MonoBehaviour
{
    private ShopController shopController;



    public void InitializeShopController(ShopController _controller) => shopController = _controller;
}
