using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerCondition : MonoBehaviour
{
    public Image hp_Bar;
    public Image mp_Bar;
    public Slider exp_Bar;

    public TextMeshProUGUI hp_Text;
    public TextMeshProUGUI mp_Text;

    private void Update()
    {
        exp_Bar.value = Manager.instance.playerStat_Manager.curExp / Manager.instance.playerStat_Manager.maxExp;
        hp_Bar.fillAmount= Manager.instance.playerStat_Manager.curHp / Manager.instance.playerStat_Manager.maxHP;
        mp_Bar.fillAmount = Manager.instance.playerStat_Manager.curMP / Manager.instance.playerStat_Manager.maxMP;

        hp_Text.text = $"{Manager.instance.playerStat_Manager.maxHP} / {Manager.instance.playerStat_Manager.curHp}";
        mp_Text.text = $"{Manager.instance.playerStat_Manager.maxMP} / {Manager.instance.playerStat_Manager.curMP}";
    }
}
