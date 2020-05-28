using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SQuest
{
    string Name;
    bool IsSuccess;
    int Reward;
    string Description;

    SQuest(string Name, bool IsSuccess, int Reward, string Description)
    {
        this.Name = Name;
        this.IsSuccess = IsSuccess;
        this.Reward = Reward;
        this.Description = Description;
    }

    SQuest(SQuest Quest)
    {
        this.Name = Quest.Name;
        this.IsSuccess = Quest.IsSuccess;
        this.Reward = Quest.Reward;
        this.Description = Quest.Description;
    }
}
public class MStructure : MonoBehaviour
{

}
