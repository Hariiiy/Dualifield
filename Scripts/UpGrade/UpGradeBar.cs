using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UpGradeBar : MonoBehaviour
{
    [SerializeField] PlayerData playerData;
    [SerializeField] Image UGforeground1;
    [SerializeField] Image UGforeground2;
    [SerializeField] Image UGforeground3;
    [SerializeField] Image UGforeground4;
    [SerializeField] Image UGforeground5;

    [SerializeField] GameObject UpGradePanel;
    [SerializeField] GameObject NoMoneyText;
    [SerializeField] TMP_Text GoldNum;

    private int BuffPrice = 100;

    private void Start()
    {
        UpGradePanel.SetActive(false);
        NoMoneyText.SetActive(false);
    }



    void Update()
    {
        float UGRatio = (float)playerData.CurrentBuffHPLevel / playerData.MaxBuffHPLevel;
        UGforeground1.transform.localScale = new Vector3(UGRatio, 1, 1);

        UGRatio = (float)playerData.CurrentBuffArmorLevel / playerData.MaxBuffArmorLevel;
        UGforeground2.transform.localScale = new Vector3(UGRatio, 1, 1);

        UGRatio = (float)playerData.CurrentBuffDamageLevel / playerData.MaxBuffDamageLevel;
        UGforeground3.transform.localScale = new Vector3(UGRatio, 1, 1);

        UGRatio = (float)playerData.CurrentBuffAccuracyLevel / playerData.MaxBuffAccuracyLevel;
        UGforeground4.transform.localScale = new Vector3(UGRatio, 1, 1);

        UGRatio = (float)playerData.CurrentBuffBulletNumLevel / playerData.MaxBuffBulletNumLevel;
        UGforeground5.transform.localScale = new Vector3(UGRatio, 1, 1);

        GoldNum.text = playerData.PlayerDataGold.ToString();
    }

    public void OnExitPanelButtonPress()
    {
        UpGradePanel.SetActive(false);
    }


    public void OnHPUpClick()
    {
        if (playerData.PlayerDataGold >= BuffPrice)
        {
            playerData.PlayerDataGold -= BuffPrice;
            playerData.CurrentBuffHPLevel++;
        }
        else
        {
            DisplayNoMoney();
        }
    }

    public void OnArmorUpClick()
    {
        if (playerData.PlayerDataGold >= BuffPrice)
        {
            playerData.PlayerDataGold -= BuffPrice;
            playerData.CurrentBuffArmorLevel++;
        }
        else
        {
            DisplayNoMoney();
        }
    }

    public void OnDamageUpClick()
    {
        if (playerData.PlayerDataGold >= BuffPrice)
        {
            playerData.PlayerDataGold -= BuffPrice;
            playerData.CurrentBuffDamageLevel++;
        }
        else
        {
            DisplayNoMoney();
        }
    }

    public void OnAccuracyUpClick()
    {
        if (playerData.PlayerDataGold >= BuffPrice)
        {
            playerData.PlayerDataGold -= BuffPrice;
            playerData.CurrentBuffAccuracyLevel++;
        }
        else
        {
            DisplayNoMoney();
        }
    }

    public void OnBulletNumUpClick()
    {
        if (playerData.PlayerDataGold >= BuffPrice)
        {
            playerData.PlayerDataGold -= BuffPrice;
            playerData.CurrentBuffBulletNumLevel++;
        }
        else
        {
            DisplayNoMoney();
        }
    }


    public void DisplayNoMoney()
    {
        NoMoneyText.SetActive(true);
        StartCoroutine(ShowTextCoroutine());
    }

    IEnumerator ShowTextCoroutine()
    {
        yield return new WaitForSeconds(1f);
        NoMoneyText.SetActive(false);
    }
}
