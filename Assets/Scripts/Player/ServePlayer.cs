using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServePlayer : MonoBehaviour
{
    public GameObject[] weapons;

    private void Update()
    {
        if (Manager.instance.equip_Manager.equipSlot[2].GetComponentInChildren<Item_Action>() != null)
        {
            for(int i = 0; i < weapons.Length; i++)
            {
                weapons[i].SetActive(true);
                Manager.instance.equip_Manager.isWeaponEquip = true;
            }
        }
        else if(Manager.instance.equip_Manager.equipSlot[2].GetComponentInChildren<Item_Action>() == null)
        {
            for (int i = 0; i < weapons.Length; i++)
            {
                weapons[i].SetActive(false);
                Manager.instance.equip_Manager.isWeaponEquip = false;
            }
        }
    }
}
