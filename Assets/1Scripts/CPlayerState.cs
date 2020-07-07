using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class CPlayerState
{
    public List<SQuest> CurrentQuest = new List<SQuest>();
    public MCar CurrentCar;
    public MQuestList QuestSlotList;

    public void Init() //Initialize datas
    {
        CurrentQuest.Capacity = 10;
    }

    public void LoadQuestSlotList(MQuestList QuestSlotList)
    {
        this.QuestSlotList = QuestSlotList;
    }
}
