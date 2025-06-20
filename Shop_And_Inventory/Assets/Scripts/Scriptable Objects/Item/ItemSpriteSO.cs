using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/BGSprite", fileName = "BGData")]
public class ItemSpriteSO : ScriptableObject
{
    [Header("BG Image")]
    public Sprite legendaryBG;
    public Sprite epicBG;
    public Sprite rareBG;
    public Sprite commonBG;
    public Sprite veryCommonBG;
}
