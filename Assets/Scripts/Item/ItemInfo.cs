using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemKind { Weapon = 0, Potion }
public class ItemInfo : MonoBehaviour
{
    public ItemKind itemKind;

    public int itemNum;

    public Sprite itemSprite;
    public int cnt = 1;

    public float atkBonus;
    public float defBonus;

    public float buyPrice;
    public bool isEquip;
}
