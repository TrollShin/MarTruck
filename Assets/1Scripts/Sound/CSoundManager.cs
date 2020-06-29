using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CSoundManager
{
    private static CSoundManager Instance;
    /*public static CSoundManager GetInstance()
    {
        if(Instance == null)
        {
            Instance = new CSoundManager();
        }

        return Instance;
    }*/

    public float EffectVolume;

    public float BackgroundVolume;

    private Dictionary<string, SSound> SoundList;

    private AudioSource EffectPlayer
    {
        get
        {
            if (EffectPlayer == null)
            {
                Scene scene = SceneManager.GetActiveScene();
                EffectPlayer = scene.GetRootGameObjects()[0].GetComponent<AudioSource>();
                if(EffectPlayer == null)
                {
                    EffectPlayer = scene.GetRootGameObjects()[0].AddComponent<AudioSource>();
                }
            }

            return EffectPlayer;
        }
        set
        {
            EffectPlayer = value;
        }
    }

    private AudioSource BackgroundPlayer
    {
        get
        {
            if (BackgroundPlayer == null)
            {
                Scene scene = SceneManager.GetActiveScene();
                BackgroundPlayer = scene.GetRootGameObjects()[0].GetComponent<AudioSource>();
                if (BackgroundPlayer == null)
                {
                    BackgroundPlayer = scene.GetRootGameObjects()[0].AddComponent<AudioSource>();
                }
            }

            return BackgroundPlayer;
        }
        set
        {
            BackgroundPlayer = value;
        }
    }

    public static void AddSound(SSoundInfo SoundInfo)
    {
        if(!Instance.SoundList.Contains(new KeyValuePair<string, SSound>(SoundInfo.Id, SoundInfo.Sound)))
            Instance.SoundList.Add(SoundInfo.Id, SoundInfo.Sound);
    }

    public static void PlaySound(string Id)
    {
        SSound Sound = Instance.SoundList[Id];
        if(Sound.Type == ESoundType.Effect)
        {
            Instance.EffectPlayer.PlayOneShot(Sound.Clip);
        }
        else if(Sound.Type == ESoundType.Background)
        {
            Instance.BackgroundPlayer.Stop();
            Instance.BackgroundPlayer.clip = Sound.Clip;
            Instance.BackgroundPlayer.Play();
        }
    }

    public static void RemoveSound(string Id)
    {
        Instance.SoundList.Remove(Id);
    }
}

[System.Serializable]
public struct SSound
{
    public AudioClip Clip;
    public ESoundType Type;
}

[System.Serializable]
public struct SSoundInfo
{
    public string Id;
    public SSound Sound;
}

public enum ESoundType
{
    Effect,
    Background
}