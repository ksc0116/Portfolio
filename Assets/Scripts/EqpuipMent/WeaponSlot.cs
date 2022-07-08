using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSlot : MonoBehaviour
{
    private void Update()
    {
        if (transform.GetComponentInChildren<Item_Action>() == null && Manager.instance.inven_Manager.isDragging==false)
        {
            Manager.instance.playerStat_Manager.atk_Bonus = 0f;
        }
    }
}
