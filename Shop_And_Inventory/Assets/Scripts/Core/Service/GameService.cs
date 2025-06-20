using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameService : GenericSingleton<GameService>
{

    [SerializeField] private UIManager uiManager;
    public UIManager UIManager => uiManager;

    [SerializeField] List<Sprite> buttonRaritySpritesList;

    [SerializeField] private ItemDataBase itemDatabase;
    private DataSevice dataSevice;

    private void Start() => dataSevice = new DataSevice(itemDatabase);

    public Sprite GetButtonRarity(Rarity _rarity) => buttonRaritySpritesList[(int)_rarity];
    public List<ItemData> GetGameItemList() => dataSevice.GetAllGameItemsList();
}
