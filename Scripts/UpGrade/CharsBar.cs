using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharsBar : MonoBehaviour
{
    [SerializeField] PlayerData playerData;

    [SerializeField] GameObject CharacterPanel;
    [SerializeField] GameObject NoMoneyText;
    [SerializeField] GameObject BuyButton;
    [SerializeField] TMP_Text GoldNum;
    [SerializeField] GameObject Buyinfo;
    [SerializeField] GameObject arrow1;
    [SerializeField] GameObject arrow2;
    [SerializeField] GameObject arrow3;
    bool owncharacter = false;

    private int CharacterPrice = 2000;

    private void Start()
    {
        CharacterPanel.SetActive(false);
        NoMoneyText.SetActive(false);
        resetArrow();
        if(playerData.CurrentCharacter == 1)
        {
            arrow1.SetActive(true);
        }
        else if (playerData.CurrentCharacter == 2)
        {
            arrow2.SetActive(true);
        }
        else if (playerData.CurrentCharacter == 3)
        {
            arrow3.SetActive(true);
        }
    }



    void Update()
    {
        GoldNum.text = playerData.PlayerDataGold.ToString();
    }

    public void OnExitPanelButtonPress()
    {
        CharacterPanel.SetActive(false);
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

    public void OnChars1ButtonClick()
    {
        playerData.CurrentCharacter = 1;
        resetArrow();
        arrow1.SetActive(true);
    }

    public void OnChars2ButtonClick()
    {
        playerData.CurrentCharacter = 2;
        resetArrow();
        arrow2.SetActive(true);
    }

    public void OnChars3ButtonClick()
    {
        if (owncharacter)
        {
            resetArrow();
            playerData.CurrentCharacter = 3;
            arrow3.SetActive(true);
        }
    }

    public void BuyButtonClick()
    {
        if (!owncharacter)
        {
            if (playerData.PlayerDataGold >= CharacterPrice)
            {
                playerData.PlayerDataGold -= CharacterPrice;
                owncharacter = true;
                BuyButton.SetActive(false);
                Buyinfo.SetActive(false);
            }
            else
            {
                DisplayNoMoney();
            }
        }
        
    }


    public void resetArrow()
    {
        arrow1.SetActive(false);
        arrow2.SetActive(false);
        arrow3.SetActive(false);
    }
}
