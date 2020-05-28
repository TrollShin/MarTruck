using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SStoreInfo
{
    List<Quest> QuestList;
    EStoreLV StoreLV;

    SStoreInfo(List<Quest> QuestList, EStoreLV StoreLV)
    {
        this.QuestList = QuestList.ToArray().Clone() as List<Quest>;
        this.StoreLV = StoreLV;
    }

    SStoreInfo(SStoreInfo StoreInfo)
    {
        this.QuestList = StoreInfo.QuestList;
        this.StoreLV = StoreInfo.StoreLV;
    }
}

public enum EStoreLV
{
    MomNPopStore,
    Mart,
    DepartmentStore,
}

public class MStore : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
