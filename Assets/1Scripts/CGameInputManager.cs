using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EMouseReversal
{
    Basic = 1,
    Reverse = -1,
}

public class CGameInputManager
{
    private static CGameInputManager instance = null;
    
    public static CGameInputManager GetInstance()
    {
        if(instance == null)
        {
            instance = new CGameInputManager();
        }

        return instance;
    }

    public float RotationSensitivity = 6f;

    public EMouseReversal MouseReversal = EMouseReversal.Basic;
}
