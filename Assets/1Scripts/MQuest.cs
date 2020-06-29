using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MQuest : MonoBehaviour
{

    public List<SQuest> QuestList = new List<SQuest>();

    private float CreateTime = 3f;

    private void Start()
    {
        StartCoroutine(AddQuest());
    }

    IEnumerator AddQuest()
    {
        while(true)
        {
            CreateQuest();
            yield return new WaitForSeconds(CreateTime);
        }
    }

    private void CreateQuest()
    {
        CQuestDBManager.GetInstance().DBCreate();

        List<SQuest> temp = CQuestDBManager.GetInstance().ReadAllQuest();
        int random = Random.Range(0, temp.Count);
        SQuest item = new SQuest(temp[random]);
        item.IsSuccess = false;
        item.Reward = Random.Range(10,51);

        Debug.Log(item.Name + " / " + item.Reward);

        QuestList.Add(item);
    }
}
