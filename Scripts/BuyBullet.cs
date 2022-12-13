using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyBullet : MonoBehaviour
{
    [SerializeField] private Behavior player;
    [SerializeField] private GameObject NoMoneyText;
    [SerializeField] private SkillManager skillManager;

    void Start()
    {
        NoMoneyText.SetActive(false);
    }

    public void OnBuy1ButtonClick()
    {
        if(player.Gold > 0)
        {
            player.Gold --;
            player.BulletQuantity++;
        }
        else
        {
            NoMoneyText.SetActive(true);
            StartCoroutine(ShowTextCoroutine());
        }
    }

    public void OnBuy10ButtonClick()
    {
        if (player.Gold >= 10)
        {
            player.Gold -= 10;
            player.BulletQuantity += 10;
        }
        else
        {
            NoMoneyText.SetActive(true);
            StartCoroutine(ShowTextCoroutine());
        }
    }

    public void OnReRollButtonClick()
    {
        if (player.Gold >= 15)
        {
            player.Gold -= 15;
            skillManager.ExitSkillMenu();
            skillManager.PickSkills();
        }
        else
        {
            NoMoneyText.SetActive(true);
            StartCoroutine(ShowTextCoroutine());
        }
    }

    IEnumerator ShowTextCoroutine()
    {
        yield return new WaitForSeconds(1f);
        NoMoneyText.SetActive(false);
    }

}
