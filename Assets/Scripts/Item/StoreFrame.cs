using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StoreFrame : MonoBehaviour
{
    public GameObject invenObj;
    public GameObject inCamera;
    Animator anim;
    Transform curParent;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        anim.SetTrigger("onAble");
    }

    public void BuyButton(ItemInfo itemInfo)
    {
        if (itemInfo.buyPrice > Manager.instance.playerStat_Manager.playerGold) return;

        Manager.instance.inven_Manager.GetItem(itemInfo);
        Manager.instance.playerStat_Manager.playerGold -= itemInfo.buyPrice;
    }

    

    public void ExitButton()
    {
        gameObject.SetActive(false);
        invenObj.SetActive(false);
        Manager.instance.camera_Manager.OnMainCamera(inCamera);
    }
}
