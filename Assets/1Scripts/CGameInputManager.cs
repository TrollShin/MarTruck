using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EMouseReversal
{
    Basic = 1,
    Reverse = -1,
}

public class CGameInputManager : MonoBehaviour
{
    public static CGameInputManager instance = null;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        //DontDestroyOnLoad(gameObject);
    }

    public float RotationSensitivity = 6f;

    public EMouseReversal MouseReversal = EMouseReversal.Basic;
}
