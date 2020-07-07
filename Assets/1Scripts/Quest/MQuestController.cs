using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MQuestController
{
    public delegate void OnAddQuest(SQuest Quest);
    public static OnAddQuest AddEvent;

    private float CreateTime = 1f;

    private List<SQuest> AllQuestList = new List<SQuest>();

    private GameObject Floors;

    private int[,] ExceptionPos = new int[,]{
        { 0, 0 },
        { 1, 1 },
        { 2, 4 },
        { 2, -4},
        { -2, 1 },
        { -3, -2 },
    };

    public MQuestController(List<SQuest> AllQuests, GameObject TargetFloors)
    {
        for (int i = 0; i < AllQuests.Count; i++)
        {
            AllQuestList.Add(AllQuests[i]);
        }
        Floors = TargetFloors;
    }

    //Quest를 랜덤으로 생성해서 리스트에 추가해주는 함수.
    public IEnumerator CreateQuestCoroutine()
    {
        WaitForSecondsRealtime wait = new WaitForSecondsRealtime(CreateTime);
        while (true)
        {
            if (MGameplayStatic.GetPlayerState().CurrentQuest.Count < 10)
            {
                SQuest RandomQuest = GetRandomQuest(AllQuestList);

                AddQuest(RandomQuest);
            }
            yield return wait;
        }
    }

    //List<SQuest> 안에서 랜덤으로 퀘스트를 뽑아주는 함수.
    private SQuest GetRandomQuest(List<SQuest> quests)
    {
        List<SQuest> PossibleQuestList = new List<SQuest>();
        foreach (SQuest temp in quests)
        {
            if (temp.LimitLV <= CUserInfo.GetInstance().StoreLv)
            {
                PossibleQuestList.Add(temp);
            }
        }

        int QuestIndexRandom = Random.Range(0, PossibleQuestList.Count);
        int xPosRandom = 0;
        int yPosRandom = 0;
        int StructureRandom = -1;
        bool isSuccessRandom = false;
        while (!isSuccessRandom)
        {
            xPosRandom = Random.Range(-CUserInfo.GetInstance().StoreLv - 1, CUserInfo.GetInstance().StoreLv + 2);
            yPosRandom = Random.Range(-CUserInfo.GetInstance().StoreLv - 1, CUserInfo.GetInstance().StoreLv + 2);

            for (int i = 0; i < ExceptionPos.GetLength(0); i++)
            {
                if (xPosRandom == ExceptionPos[i, 0] && yPosRandom == ExceptionPos[i, 1])
                {
                    break;
                }
                if (i == ExceptionPos.GetLength(0) - 1)
                {
                    for (int j = 0; j < Floors.transform.childCount; j++)
                    {
                        string name = Floors.transform.GetChild(j).gameObject.name;
                        string[] split = name.Split(',');
                        string[] splitL = split[0].Split('(');
                        string[] splitR = split[1].Split(')');
                        if (xPosRandom.ToString().Equals(splitL[1]) && yPosRandom.ToString().Equals(splitR[0]))
                        {
                            MStructure[] Structures = Floors.transform.GetChild(j).GetComponentsInChildren<MStructure>();
                            if(Structures.Length != 0)
                            {
                                StructureRandom = Random.Range(0, Structures.Length);
                                isSuccessRandom = true;
                            }
                        }
                    }
                }
            }
        }

        

        SQuest item = new SQuest(PossibleQuestList[QuestIndexRandom]);
        item.TargetPos = new int[3] { xPosRandom, yPosRandom, StructureRandom };
        item.IsSuccess = false;
        item.Reward = Mathf.Abs(xPosRandom) + Mathf.Abs(yPosRandom);

        return item;
    }

    //SQuest를 스크롤뷰에 쓰는 리스트에 추가해주는 함수.
    private void AddQuest(SQuest item)
    {
        if (MGameplayStatic.GetPlayerState() == null) return;
        MGameplayStatic.GetPlayerState().CurrentQuest.Add(item);
        if (AddEvent != null)
        {
            AddEvent(item);
        }
    }
}
