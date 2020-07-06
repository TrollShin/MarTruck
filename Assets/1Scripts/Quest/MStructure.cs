using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public struct SQuest
{
    public int QuestIndex;
    public int[] TargetPos;
    public string Name;
    public bool IsSuccess;
    public int Reward;
    public string Description;
    public int LimitLV;

    public SQuest(int QuestIndex, int[] TargetPos, string Name, bool IsSuccess, int Reward, string Description, int LimitLV)
    {
        this.QuestIndex = QuestIndex;
        this.TargetPos = TargetPos;
        this.Name = Name;
        this.IsSuccess = IsSuccess;
        this.Reward = Reward;
        this.Description = Description;
        this.LimitLV = LimitLV;
    }

    public SQuest(SQuest Quest)
    {
        this.QuestIndex = Quest.QuestIndex;
        this.TargetPos = Quest.TargetPos;
        this.Name = Quest.Name;
        this.IsSuccess = Quest.IsSuccess;
        this.Reward = Quest.Reward;
        this.Description = Quest.Description;
        this.LimitLV = Quest.LimitLV;
    }
}
public class MStructure : MonoBehaviour
{
    public Text EntranceText;

    public SQuest Quset;

    private void Awake()
    {
        EntranceText = GameObject.Find("Canvas").transform.Find("EntranceText").GetComponent<Text>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Car")
        {
            EntranceText.gameObject.SetActive(true);
            EntranceText.text = gameObject.name + " 건물에 입장하려면\n" + "V키를 눌러주세요";
            if (Input.GetKeyDown(KeyCode.V))
            {
                Time.timeScale = 0;
                CSceneFunctionLibrary.ShowStoreMenu();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Car")
        {
            EntranceText.gameObject.SetActive(false);

        }
    }
}
