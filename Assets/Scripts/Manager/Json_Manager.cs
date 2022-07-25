using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;

public class Json_Manager : MonoBehaviour 
{
    public class TempClass
    {
        public ItemInfo[] invenSlots_itemInfo= new ItemInfo[Manager.instance.invenSlot_Manager.invenSlots.Length];
        public int[] itemCountInven=new int[Manager.instance.invenSlot_Manager.invenSlots.Length];
        public ItemInfo[] quickSlots_itemInfo= new ItemInfo[Manager.instance.quickSlot_Manager.quickSlots.Length];
        public int[] itemCountQuick= new int[Manager.instance.quickSlot_Manager.quickSlots.Length];
        public ItemInfo[] equipSlots_itemInfo=new ItemInfo[Manager.instance.equipSlot_Manager.equipSlots.Length];
        public int[] itemCountEquip=new int[Manager.instance.equipSlot_Manager.equipSlots.Length];
        public Vector3 playerPosition = new Vector3(0, 0, -8f);

        public float curHp = Manager.instance.playerStat_Manager.maxHP;
        public float curMp= Manager.instance.playerStat_Manager.maxMP;
        public float curExp = 0f;
        public float curGold = 200f;
        public int curLev = 1;
        public float maxHp = 100f;
        public float maxMp = 100f;
        public float atk = 30f;
        public float originAtk = 30f;
        public int qSkillDamage = 50;
        public int wSkillDamage = 30;
        public float qSkillUseMp = 10f;
        public float wSkillUseMp = 20f;

        public int quest_dieTutleShell=0;
        public int quest_dieBeetle=0;
        public int quest_dieBoss=0;
        public int questIndex=0;
        public bool isAccept=false;
        public bool isClear = false;
        public bool isFirstEvent=false;
        public bool isSecEvent=false;

        public AudioClip curBGM = Manager.instance.sound_Manager.bgmAudioSource.clip;


        public TempClass()
        {
            for(int i = 0; i < invenSlots_itemInfo.Length; i++)
            {
                invenSlots_itemInfo[i] = null;
                itemCountInven[i] = 1;
            }
            for(int i = 0; i < quickSlots_itemInfo.Length; i++)
            {
                quickSlots_itemInfo[i] = null;
                itemCountQuick[i] = 1;  
            }
            for(int i = 0; i < equipSlots_itemInfo.Length; i++)
            {
                equipSlots_itemInfo [i] = null;
                itemCountEquip[i] = 1;  
            }
        }
    }

    public ItemInfo[] invenSlots_itemInfo;
    public int[] itemCountInven;
    public ItemInfo[] quickSlots_itemInfo;
    public int[] itemCountQuick;
    public ItemInfo[] equipSlots_itemInfo;
    public int[] itemCountEquip;
    public Transform player;

    public Transform[] invenSlots;
    public Transform[] quickSlots;
    public Transform[] equipSlots;

    TempClass tempClass;
    string jsonStr;
    private void Awake()
    {
        itemCountInven = new int[invenSlots.Length];
        itemCountQuick=new int[quickSlots.Length];
        itemCountEquip=new int[equipSlots.Length];

        tempClass = new TempClass();
    }
    public void JsonDataSave()
    {
        for (int i = 0; i < invenSlots.Length; i++)
        {
            if (Manager.instance.invenSlot_Manager.invenSlots[i].GetChild(0).GetComponent<Item_Action>().m_itemInfo == null) continue;


            tempClass.invenSlots_itemInfo[i] = Manager.instance.invenSlot_Manager.invenSlots[i].GetChild(0).GetComponent<Item_Action>().m_itemInfo;
            tempClass.itemCountInven[i] = tempClass.invenSlots_itemInfo[i].Cnt;
        }

        for (int i = 0; i < quickSlots.Length; i++)
        {
            if (Manager.instance.quickSlot_Manager.quickSlots[i].GetChild(1).GetComponent<Item_Action>().m_itemInfo == null) continue;
            

            tempClass.quickSlots_itemInfo[i] = Manager.instance.quickSlot_Manager.quickSlots[i].GetChild(1).GetComponent<Item_Action>().m_itemInfo;
            tempClass.itemCountQuick[i] = tempClass.quickSlots_itemInfo[i].Cnt;
        }

        for (int i = 0; i < equipSlots.Length; i++)
        {
            if (Manager.instance.equipSlot_Manager.equipSlots[i].GetChild(1).GetComponent<Item_Action>().m_itemInfo == null) continue;

            tempClass.equipSlots_itemInfo[i] = Manager.instance.equipSlot_Manager.equipSlots[i].GetChild(1).GetComponent<Item_Action>().m_itemInfo;
            tempClass.itemCountEquip[i] = tempClass.equipSlots_itemInfo[i].Cnt;
        }

        tempClass.curExp = Manager.instance.playerStat_Manager.curExp;
        tempClass.curHp = Manager.instance.playerStat_Manager.curHp;
        tempClass.curMp = Manager.instance.playerStat_Manager.curMP;
        tempClass.curGold = Manager.instance.playerStat_Manager.playerGold;
        tempClass.curLev = Manager.instance.playerStat_Manager.lev;
        tempClass.qSkillDamage = Manager.instance.playerStat_Manager.qSkillDamage;
        tempClass.wSkillDamage = Manager.instance.playerStat_Manager.wSkillDamage;
        tempClass.qSkillUseMp = Manager.instance.playerStat_Manager.qSkillUseMp;
        tempClass.wSkillUseMp = Manager.instance.playerStat_Manager.wSkillUseMp;

        tempClass.playerPosition = Manager.instance.playerStat_Manager.player.transform.position;

        tempClass.quest_dieTutleShell = Manager.instance.quest_Manager.dieTurtleShellCnt;
        tempClass.quest_dieBeetle = Manager.instance.quest_Manager.dieBeetleCnt;
        tempClass.quest_dieBoss = Manager.instance.quest_Manager.dieBossCnt;
        tempClass.questIndex = Manager.instance.quest_Manager.questIndex;
        tempClass.isAccept = Manager.instance.quest_Manager.isAccept;
        tempClass.isClear = Manager.instance.quest_Manager.isClear;
        tempClass.isFirstEvent = Manager.instance.quest_Manager.isFirstEvent;
        tempClass.isSecEvent = Manager.instance.quest_Manager.isSecEvent;
        tempClass.maxHp = Manager.instance.playerStat_Manager.maxHP;
        tempClass.maxMp = Manager.instance.playerStat_Manager.maxMP;
        tempClass.atk = Manager.instance.playerStat_Manager.atk;
        tempClass.originAtk = Manager.instance.playerStat_Manager.originAtk;

        tempClass.curBGM = Manager.instance.sound_Manager.bgmAudioSource.clip;

        jsonStr = JsonUtility.ToJson(tempClass);
        PlayerPrefs.SetString("jsonStr", jsonStr);
    }
    public void JsonDataLoad()
    {
        if (PlayerPrefs.HasKey("jsonStr") == false)
        {
            JsonDataReset();
            return;
        }

        jsonStr = PlayerPrefs.GetString("jsonStr");
        tempClass = JsonUtility.FromJson<TempClass>(jsonStr);

        for (int i = 0; i < tempClass.invenSlots_itemInfo.Length; i++)
        {
            if (tempClass.invenSlots_itemInfo[i] == null)
            {
                itemCountInven[i] = tempClass.itemCountInven[i];
                invenSlots[i].GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = itemCountInven[i].ToString();
                invenSlots[i].GetChild(0).GetComponent<Item_Action>().m_itemInfo = null;
                invenSlots[i].GetChild(0).GetComponent<Image>().sprite = null;
                invenSlots[i].GetChild(0).gameObject.SetActive(false);
                continue;
            }

            itemCountInven[i] = tempClass.itemCountInven[i];
            invenSlots[i].GetChild(0).GetComponent<Item_Action>().m_itemInfo = tempClass.invenSlots_itemInfo[i];
            invenSlots[i].GetChild(0).GetComponent<Item_Action>().m_itemInfo.Cnt =itemCountInven[i];
            invenSlots[i].GetChild(0).GetComponent<Image>().sprite = invenSlots[i].GetChild(0).GetComponent<Item_Action>().m_itemInfo.itemSprite;
            invenSlots[i].GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = invenSlots[i].GetChild(0).GetComponent<Item_Action>().m_itemInfo.Cnt.ToString();
            invenSlots[i].GetChild(0).gameObject.SetActive(true);
        }

        for (int i = 0; i < tempClass.quickSlots_itemInfo.Length; i++)
        {
            if (tempClass.quickSlots_itemInfo[i] == null)
            {
                itemCountQuick[i] = tempClass.itemCountQuick[i];
                quickSlots[i].GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = itemCountQuick[i].ToString();
                quickSlots[i].GetChild(1).GetComponent<Item_Action>().m_itemInfo = null;
                quickSlots[i].GetChild(1).GetComponent<Image>().sprite = null;
                quickSlots[i].GetChild(1).gameObject.SetActive(false);
                continue;
            }

            itemCountQuick[i] = tempClass.itemCountQuick[i];
            quickSlots[i].GetChild(1).GetComponent<Item_Action>().m_itemInfo = tempClass.quickSlots_itemInfo[i];
            quickSlots[i].GetChild(1).GetComponent<Item_Action>().m_itemInfo.Cnt = itemCountQuick[i];
            quickSlots[i].GetChild(1).GetComponent<Image>().sprite = quickSlots[i].GetChild(1).GetComponent<Item_Action>().m_itemInfo.itemSprite;
            quickSlots[i].GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = quickSlots[i].GetChild(1).GetComponent<Item_Action>().m_itemInfo.Cnt.ToString();
            quickSlots[i].GetChild(1).gameObject.SetActive(true);
        }

        for (int i = 0; i < tempClass.equipSlots_itemInfo.Length; i++)
        {
            if (tempClass.equipSlots_itemInfo[i] == null)
            {
                itemCountEquip[i] = tempClass.itemCountEquip[i];
                equipSlots[i].GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = itemCountEquip[i].ToString();
                equipSlots[i].GetChild(1).GetComponent<Item_Action>().m_itemInfo = null;
                equipSlots[i].GetChild(1).GetComponent<Image>().sprite = null;
                equipSlots[i].GetChild(1).gameObject.SetActive(false);
                continue;
            }

            itemCountEquip[i] = tempClass.itemCountEquip[i];
            equipSlots[i].GetChild(1).GetComponent<Item_Action>().m_itemInfo = tempClass.equipSlots_itemInfo[i];
            equipSlots[i].GetChild(1).GetComponent<Item_Action>().m_itemInfo.Cnt = itemCountEquip[i];
            equipSlots[i].GetChild(1).GetComponent<Image>().sprite = equipSlots[i].GetChild(1).GetComponent<Item_Action>().m_itemInfo.itemSprite;
            equipSlots[i].GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = equipSlots[i].GetChild(1).GetComponent<Item_Action>().m_itemInfo.Cnt.ToString();
            equipSlots[i].GetChild(1).gameObject.SetActive(true);
        }

        Manager.instance.playerStat_Manager.curMP = tempClass.curMp;
        Manager.instance.playerStat_Manager.curHp = tempClass.curHp;
        Manager.instance.playerStat_Manager.curExp = tempClass.curExp;
        Manager.instance.playerStat_Manager.playerGold = tempClass.curGold;
        Manager.instance.playerStat_Manager.lev = tempClass.curLev;
        Manager.instance.playerStat_Manager.maxHP = tempClass.maxHp;
        Manager.instance.playerStat_Manager.maxMP = tempClass.maxMp;
        Manager.instance.playerStat_Manager.atk = tempClass.atk;
        Manager.instance.playerStat_Manager.originAtk = tempClass.originAtk;
        Manager.instance.playerStat_Manager.qSkillDamage = tempClass.qSkillDamage;
        Manager.instance.playerStat_Manager.wSkillDamage = tempClass.wSkillDamage;
        Manager.instance.playerStat_Manager.qSkillUseMp = tempClass.qSkillUseMp;
        Manager.instance.playerStat_Manager.wSkillUseMp = tempClass.wSkillUseMp;

        Manager.instance.playerStat_Manager.player.GetComponent<NavMeshAgent>().enabled = false;
        Manager.instance.playerStat_Manager.player.transform.position = tempClass.playerPosition;
        Manager.instance.playerStat_Manager.player.GetComponent<NavMeshAgent>().enabled = true;

        Manager.instance.quest_Manager.dieTurtleShellCnt = tempClass.quest_dieTutleShell;
        Manager.instance.quest_Manager.dieBeetleCnt = tempClass.quest_dieBeetle;
        Manager.instance.quest_Manager.dieBossCnt = tempClass.quest_dieBoss;
        Manager.instance.quest_Manager.questIndex = tempClass.questIndex;
        Manager.instance.quest_Manager.isAccept = tempClass.isAccept;
        Manager.instance.quest_Manager.isClear = tempClass.isClear;
        Manager.instance.quest_Manager.isFirstEvent = tempClass.isFirstEvent;
        Manager.instance.quest_Manager.isSecEvent = tempClass.isSecEvent;

        Manager.instance.sound_Manager.ChangeBGM(tempClass.curBGM);
    }
    public void JsonDataReset()
    {
        TempClass tempClasee2 = new TempClass();
        jsonStr = JsonUtility.ToJson(tempClasee2);
        PlayerPrefs.SetString("jsonStr", jsonStr);
    }
}