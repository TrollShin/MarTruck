using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MQuestList : MonoBehaviour
{
    public Object QuestSlot;

    private void Awake()
    {
        MQuestUIFunctionLibrary.AddListEvent += AddQuest;
    }

    private void OnDisable()
    {
        MQuestUIFunctionLibrary.AddListEvent -= AddQuest;
    }

    public void UpdateQuestList(List<SQuest> QuestList)
    {
        ClearQuestList();
        foreach(SQuest Quest in QuestList)
        {
            AddQuest(Quest);
        }
    }

    public void AddQuest(SQuest Quest)
    {
        GameObject Obj = Instantiate(QuestSlot) as GameObject;
        Obj.transform.SetParent(gameObject.transform);
        Obj.GetComponent<MQuestSlot>().UpdateQuestInfo(Quest);
        RectTransform rectTransform = GetComponent<RectTransform>();
        VerticalLayoutGroup layoutGroup = GetComponent<VerticalLayoutGroup>();
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, rectTransform.sizeDelta.y + Obj.GetComponent<RectTransform>().sizeDelta.y + layoutGroup.spacing);
    }

    public void ClearQuestList()
    {
        transform.DetachChildren();
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, 0);
    }
}
