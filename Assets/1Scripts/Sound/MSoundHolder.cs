using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Sound 저장 및 삭제
public class MSoundHolder : MonoBehaviour
{
    public List<SSoundInfo> SoundInfoList;


    // Start is called before the first frame update
    void Awake()
    {
        foreach(SSoundInfo SoundInfo in SoundInfoList)
        {
            CSoundManager.AddSound(SoundInfo);
        }
    }

    private void OnDestroy()
    {
        foreach(SSoundInfo SoundInfo in SoundInfoList)
        {
            CSoundManager.RemoveSound(SoundInfo.Id);
        }
    }
}
