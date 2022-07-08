using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Quest_Manager : MonoBehaviour
{
    public int questIndex = 0;

    [SerializeField] GameObject talkText;

    [SerializeField] GameObject inCamera;

    [SerializeField] GameObject questPanel;
    [SerializeField] GameObject acceptButtonObj;
    [SerializeField] GameObject denyButtonObj;
    [SerializeField] GameObject clearButtonObj;
    [SerializeField] GameObject backButtonObj;

    [SerializeField] TurtleShellSpawner turtleShellSpawner;
    public int dieTurtleShellCnt;
    [SerializeField] GameObject[] beetleCount;
    public int dieBeetleCnt;

    [SerializeField] TextMeshProUGUI questTitleText;
    [SerializeField] TextMeshProUGUI questContentText;
    [SerializeField] TextMeshProUGUI questTargetText;
    [SerializeField] TextMeshProUGUI questRewardText;
    public int[] rewardGold;
    public float[] rewardExp;

    [SerializeField] TextMeshProUGUI cur_questTitle;
    [SerializeField] TextMeshProUGUI cur_questTarget;

    public string questTitle;
    public string questContent;
    public string questTarget;
    public string questReward;

    public bool isAccept;

    public bool isClear = false;

    public int targetSwordCnt = 1;
    public int swordCnt = 0;

    private void Awake()
    {
        rewardExp = new float[2] { 50f, 100f };
        rewardGold = new int[2] { 100, 1000 };

        questTitle = "���� �����ϱ�";
        questContent = "������ ������ �ռ� ���ο��Լ� ���⸦ �����غ���.";
        questTarget = "�� 1�� ����";
        questReward = $"G : {rewardGold[0]}G       EXP : {rewardExp[0]}EXP";
        //questTitle = "���� ��ġ";
        //questContent = "���� ���� ������� �������� ���͵��� ��� óġ�������� ��";
        //questTarget = $"���� �ź� : {turtleShellSpawner.spawnPoint.Length} ����   ǳ���� : {beetleCount.Length} ����";
        //questReward = $"G : {rewardGold}G       EXP : {rewardExp}EXP";

        questTitleText.text = questTitle;
        questTargetText.text = questTarget;
        questContentText.text = questContent;
        questRewardText.text = questReward;
    }

    public void CurQuestCheck()
    {
        if (isAccept == false) return;

        questPanel.SetActive(true);
    }
    public void UpdateCurQuestText()
    {
        cur_questTitle.text = questTitle;
        if(questIndex == 0)
        {
            cur_questTitle.text = questTitle;
            cur_questTarget.text = $"�� : {swordCnt} / {targetSwordCnt}";
            questTargetText.text = $"�� : {swordCnt} / {targetSwordCnt}";
        }
        else if (questIndex == 1)
        {
            //���� ���� �ؽ�Ʈ �߰��ϱ�
            cur_questTarget.text = $"���� �ź� : {dieTurtleShellCnt} / {turtleShellSpawner.spawnPoint.Length}, ǳ���� : {dieBeetleCnt} / {beetleCount.Length}";
            questTargetText.text = $"���� �ź� : {dieTurtleShellCnt} / {turtleShellSpawner.spawnPoint.Length}, ǳ���� : {dieBeetleCnt} / {beetleCount.Length}";
        }
    }

    public void ClearButton()
    {
        isClear = false;
        isAccept = false;

        Manager.instance.playerStat_Manager.playerGold += rewardGold[questIndex];
        Manager.instance.playerStat_Manager.curExp += rewardExp[questIndex];
        Manager.instance.camera_Manager.OnMainCamera(inCamera);

        questIndex++;

        talkText.SetActive(false);

        SelectText();

        questPanel.SetActive(false);
    }
    void SelectText()
    {
        if(questIndex == 1)
        {
            questTitle = "���� ��ġ�ϱ�";
            questTitleText.text = questTitle;
            questContentText.text = "���� ������� �����ϴ� ���͵��� ��ġ����.";

            // ���� ���� �ؽ�Ʈ �߰��ϱ�
            questTargetText.text = $"���� �ź� : {dieTurtleShellCnt} / {turtleShellSpawner.spawnPoint.Length}, ǳ���� : {dieBeetleCnt} / {beetleCount.Length}";
            questRewardText.text = $"G : {rewardGold[questIndex]}G       EXP : {rewardExp[questIndex]}EXP";
        }
        else
        {
            questTitleText.text = "";
            questContentText.text = "";
            questTargetText.text = "";
            questRewardText.text = "";
        }
        cur_questTitle.text = "";
        cur_questTarget.text = "";
    }
    public void AcceptButton()
    {
        isAccept = true;
        questPanel.SetActive(false);
        talkText.SetActive(false);
        Manager.instance.camera_Manager.OnMainCamera(inCamera);

        if(questIndex == 0)
        {
            cur_questTitle.text = questTitle;
            cur_questTarget.text = $"�� : {swordCnt} / {targetSwordCnt}";
            questTargetText.text = $"�� : {swordCnt} / {targetSwordCnt}";
        }
        else if(questIndex == 1)
        {
            // ���� �ؽ�Ʈ �߰��ϱ�
            cur_questTitle.text = questTitle;
            cur_questTarget.text = $"���� �ź� : {dieTurtleShellCnt} / {turtleShellSpawner.spawnPoint.Length}, ǳ���� : {dieBeetleCnt} / {beetleCount.Length}";
            questTargetText.text = $"���� �ź� : {dieTurtleShellCnt} / {turtleShellSpawner.spawnPoint.Length}, ǳ���� : {dieBeetleCnt} / {beetleCount.Length}";
        }
    }
    public void DenyButton()
    {
        Manager.instance.camera_Manager.OnMainCamera(inCamera);
        questPanel.SetActive(false);
        talkText.SetActive(false);
    }
    public void BackButton()
    {
        Manager.instance.camera_Manager.OnMainCamera(inCamera);
        questPanel.SetActive(false);
        talkText.SetActive(false);
    }



    private void Update()
    {
        if (questIndex == 0)
        {
            if (swordCnt == 1)
            {
                isClear = true;
            }
        }

        if(questIndex == 1)
        {
            if ((dieBeetleCnt == beetleCount.Length) && (dieTurtleShellCnt == turtleShellSpawner.spawnPoint.Length))
            {
                isClear = true;
            }
        }


        if (isAccept == true)
        {
            if (isClear == true)
            {
                cur_questTitle.text = "CLEAR!";
                denyButtonObj.SetActive(false);
                acceptButtonObj.SetActive(false);
                clearButtonObj.SetActive(true);
                backButtonObj.SetActive(true);
            }
            else if (isClear == false)
            {
                denyButtonObj.SetActive(false);
                acceptButtonObj.SetActive(false);
                backButtonObj.SetActive(true);
            }
        }
        else if(isAccept == false)
        {
            cur_questTitle.text = "";
            denyButtonObj.SetActive(true);
            acceptButtonObj.SetActive(true);
            clearButtonObj.SetActive(false);
            backButtonObj.SetActive(false);
        }
    }
}
