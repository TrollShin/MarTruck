using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MQuestSlot : MonoBehaviour
{
    [SerializeField]
    private Text Title;
    [SerializeField]
    private Text Description;
    [SerializeField]
    private Toggle IsSuccessed;
    [SerializeField]
    private Text Reward;

    void UpdateQuestInfo(SQuest Quest)
    {
        Title.text = Quest.Name;
        Description.text = Quest.Description;
        IsSuccessed.isOn = Quest.IsSuccess;
        Reward.text = "KRW" + Quest.Reward.ToString();
    }
}
