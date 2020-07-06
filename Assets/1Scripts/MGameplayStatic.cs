using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MGameplayStatic : MonoBehaviour
{
    private static MGameplayStatic Instance;
    private CPlayerState PlayerState;

    private void Awake() // Allow access only scene MGameplayStatic Component has
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }

    public static CPlayerState GetPlayerState()
    {
        if (Instance == null)
            return null;

        if(Instance.PlayerState == null)
        {
            Instance.PlayerState = new CPlayerState();
            Instance.PlayerState.Init();
        }

        return Instance.PlayerState;
    }
}
