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
    public static CSoundManager GetInstance()
    {
        if(Instance == null)
        {
            Instance = new CSoundManager();
        }

        return Instance;
    }

    private float _EffectVolume = .5f;
    public float EffectVolume
    {
        get
        {
            return _EffectVolume;
        }
        set
        {
            _EffectVolume = value;
        }
    }

    private float _BackgroundVolume = .5f;
    public float BackgroundVolume
    {
        get
        {
            return _BackgroundVolume;
        }
        set
        {
            _BackgroundVolume = value;
            SoundPlayer.volume = value;
        }
    }

    private readonly Dictionary<string, SSound> SoundMap;

    private AudioSource _SoundPlayer;
    private AudioSource SoundPlayer
    {
        get
        {
            if (_SoundPlayer == null)
            {
                Scene scene = SceneManager.GetActiveScene();
                _SoundPlayer = scene.GetRootGameObjects()[0].GetComponent<AudioSource>();
                if(_SoundPlayer == null)
                {
                    _SoundPlayer = scene.GetRootGameObjects()[0].AddComponent<AudioSource>();
                }

                _SoundPlayer.loop = true;
            }

            return _SoundPlayer;
        }
        set
        {
            _SoundPlayer = value;
        }
    }

    public static void AddSound(SSoundInfo SoundInfo)
    {
        if(!HasSound(SoundInfo))
            GetInstance().SoundMap.Add(SoundInfo.Id, SoundInfo.Sound);
    }

    public static void PlaySound(string Id)
    {
        if (!HasSound(Id))
            return;

        CSoundManager Instance = GetInstance();
        SSound Sound = Instance.SoundMap[Id];

        if(Sound.Type == ESoundType.Effect)
        {
            Instance.SoundPlayer.PlayOneShot(Sound.Clip,  Instance.EffectVolume);
        }
        else if(Sound.Type == ESoundType.Background)
        {
            Instance.SoundPlayer.Stop();
            Instance.SoundPlayer.clip = Sound.Clip;
            Instance.SoundPlayer.Play();
        }
    }

    public static void RemoveSound(string Id)
    {
        if (!HasSound(Id))
            return;

        CSoundManager Manager = GetInstance();
        SSound Sound = Manager.SoundMap[Id];
        if(!Sound.IsConstant)
            Manager.SoundMap.Remove(Id);
    }

    private static bool HasSound(SSoundInfo SoundInfo)
    {
        return GetInstance().SoundMap.Keys.Contains(SoundInfo.Id);
    }

    private static bool HasSound(string Id)
    {
        return GetInstance().SoundMap.Keys.Contains(Id);
    }

    private static bool HasSound(SSound Sound)
    {
        return GetInstance().SoundMap.Values.Contains(Sound);
    }
}

[System.Serializable]
public struct SSound
{
    public AudioClip Clip;
    public ESoundType Type;
    [Tooltip("If true, It will not remove at other scene")]
    public bool IsConstant;
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