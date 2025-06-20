using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCardSpawner : MonoBehaviour
{
    [SerializeField] private GameObject ItemCardPrefab;
    [SerializeField] private ItemSpriteSO spriteSO;
    private ItemCardView cardView;

    private void Start()
    {
        GameObject itemCardHolder = CreateItemCard();
        cardView = itemCardHolder.GetComponent<ItemCardView>();

        ItemCardModel cardmodel = new ItemCardModel(spriteSO);
        ItemCardController controller = new ItemCardController(cardView, cardmodel);
    }

    private GameObject CreateItemCard()
    {
        GameObject itemCardHolder = Instantiate(ItemCardPrefab);
        itemCardHolder.transform.SetParent(transform, false);
        return itemCardHolder;
    }
}
