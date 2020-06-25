using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MOption<T> : MonoBehaviour
{
    private void Awake()
    {
        InitOption();
    }

    protected abstract void InitOption();

    protected abstract void SetOption(T value);
}
