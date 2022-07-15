using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemKind { Weapon = 0, Potion }
[System.Serializable]
public class ItemInfo : MonoBehaviour
{
    public ItemKind itemKind;

    public int itemNum;

    public Sprite itemSprite;
    int cnt = 1;
    public int Cnt { get { return cnt; } set { cnt = value; } }

    public float atkBonus;
    public float defBonus;

    public float buyPrice;
    public bool isEquip;
}
