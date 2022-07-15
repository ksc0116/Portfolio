using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager instance;

    [Header("[Manager]")]
    public Inven_Manager inven_Manager;
    public Equip_Manager equip_Manager;
    public PlayerStat playerStat_Manager;
    public Camera_Manager camera_Manager;
    public Quest_Manager quest_Manager;
    public Disable_Manager disable_Manager;
    public Sound_Manager sound_Manager;
    public QuickSlot_Manager quickSlot_Manager;
    public InvenSlot_Manager invenSlot_Manager;
    public Json_Manager json_Manager;
    public EquipSlot_Manager equipSlot_Manager;
    private void Awake()
    {
        if(instance != this)
            instance = this;
    }
}
