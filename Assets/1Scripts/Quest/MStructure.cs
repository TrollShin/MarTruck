using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SQuest
{
    public int QuestIndex;
    public string Name;
    public bool IsSuccess;
    public int Reward;
    public string Description;
    public int LimitLV;

    public SQuest(int QuestIndex, string Name, bool IsSuccess, int Reward, string Description, int LimitLV)
    {
        this.QuestIndex = QuestIndex;
        this.Name = Name;
        this.IsSuccess = IsSuccess;
        this.Reward = Reward;
        this.Description = Description;
        this.LimitLV = LimitLV;
    }

    public SQuest(SQuest Quest)
    {
        this.QuestIndex = Quest.QuestIndex;
        this.Name = Quest.Name;
        this.IsSuccess = Quest.IsSuccess;
        this.Reward = Quest.Reward;
        this.Description = Quest.Description;
        this.LimitLV = Quest.LimitLV;
    }
}
public class MStructure : MonoBehaviour
{
    public SQuest Quset;
}
