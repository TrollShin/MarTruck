using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MStructureUIFunctionLibrary : MonoBehaviour
{
    public GameObject MenuTap;
    public GameObject QuestTap;

    public Text[] QuestTexts;

    private SQuest Quest;

    

    void Awake()
    {
        MStructure.StructEvent += GetQuest;
    }

    public void OnClickExit()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("StructureUI"));
        Time.timeScale = 1;
    }

    public void GetQuest(MStructure structure)
    {

        MenuTap.SetActive(false);
        QuestTap.SetActive(true);

        QuestTexts[0].text = structure.Quest.Name;
        QuestTexts[1].text = structure.Quest.Description;
        QuestTexts[2].text = "보상: " + structure.Quest.Reward.ToString() + " KRW";
    }
    
    public void OnClickQuestbtn()
    {        
        
        //Debug.Log(StructEvent().gameObject.name);
        //Debug.Log(StructEvent().Quest.Name);
        //Debug.Log(StructEvent().Quest.Description);
        //Debug.Log("보상: " + StructEvent().Quest.Reward.ToString() + " KRW");
        
    }


    public void OnBackQuestTap()
    {
        QuestTap.SetActive(false);
        MenuTap.SetActive(true);
    }

    public void OnClickClear()
    {
        //StructEvent().Quest.IsSuccess = true;
        Time.timeScale = 1;
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("StructureUI"));
    }
}
