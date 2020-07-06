using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MDisplayMoney : MonoBehaviour
{
    public TextMeshProUGUI DisplayText;
    private int Money
    {
        set
        {
            DisplayText.text = "KRW  " + value.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        CUserInfo UserInfo = CUserInfo.GetInstance();
        if (UserInfo == null)
            return;

        Money = UserInfo.Money;
    }
}
