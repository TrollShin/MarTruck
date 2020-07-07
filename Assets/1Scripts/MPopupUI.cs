using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MPopupUI : MonoBehaviour
{
    private static MPopupUI Instance;
    private float HideTime = 3f;

    private void Awake() // Allow access only scene MGameplayStatic Component has
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }

    public static MPopupUI GetInstance()
    {
        if (Instance == null)
            return null;

        return Instance;
    }

    public GameObject PopupUI;

    public void ShowPopup(string message)
    {
        GameObject item = Instantiate(PopupUI);
        item.transform.SetParent(transform);

        item.GetComponentInChildren<TextMeshProUGUI>().text = message;

        StartCoroutine(DelayHidePopup(item));
    }

    IEnumerator DelayHidePopup(GameObject item)
    {
        yield return new WaitForSecondsRealtime(HideTime);

        Destroy(item);
    }
}
