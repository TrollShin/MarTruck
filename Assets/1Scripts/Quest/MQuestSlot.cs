using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MQuestSlot : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI Title;
    [SerializeField]
    private TextMeshProUGUI Description;
    [SerializeField]
    private Toggle IsSuccessed;
    [SerializeField]
    private TextMeshProUGUI Reward;

    public void UpdateQuestInfo(SQuest Quest)
    {
        Title.text = Quest.Name;
        Description.text = Quest.Description;
        IsSuccessed.isOn = Quest.IsSuccess;
        Reward.text = "KRW  " + Quest.Reward.ToString();
    }
}
