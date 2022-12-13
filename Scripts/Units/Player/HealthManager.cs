using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int MaxHp;
    public int Hp;
    public int MaxArmor;
    public int Armor;
    public bool InDizzy;
    bool cannotBeAttacked;
    public bool PlayerDead;


    //skill
    public bool DedicatedToDeathOn;

    [SerializeField] PlayerData playerData;
    Behavior player;
    [SerializeField] GameObject HitSound;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Behavior>();
        if(playerData.CurrentCharacter ==1 )
        {
            MaxHp = 4 + playerData.CurrentBuffHPLevel;
            MaxArmor = 5 + playerData.CurrentBuffArmorLevel;
        }
        else if (playerData.CurrentCharacter == 2)
        {
            MaxHp = 5 + playerData.CurrentBuffHPLevel;
            MaxArmor = 6 + playerData.CurrentBuffArmorLevel;
        }
        else if (playerData.CurrentCharacter == 3)
        {
            MaxHp = 7 + playerData.CurrentBuffHPLevel;
            MaxArmor = 8 + playerData.CurrentBuffArmorLevel;
        }

        Hp = MaxHp;
        Armor = MaxArmor;
        InDizzy = false;
    }


    void Update()
    {
        CheckIfDead();
    }

    public void ReceiveDamage()
    {
        if(!cannotBeAttacked)
        {
            if (Armor != 0)
            {
                Armor--;
            }
            else
            {
                Hp--;
            }
            InDizzy = true;
            StartCoroutine(ReceiveDamageColdDown());
            StartCoroutine(Dizzy());
            Instantiate(HitSound, transform.position,Quaternion.identity);
        }
        cannotBeAttacked = true;
    }
   
    void CheckIfDead()
    {      
        if(Hp <= 0 && DedicatedToDeathOn)
        {
            Hp = MaxHp;
            Armor = MaxArmor;
            DedicatedToDeathOn = false;
        }
        else if(Hp <= 0)
        {
            player.AttackVaild = false;
            PlayerDead = true;
            player.SavePlayerGoldToData();
        }
    }

    IEnumerator ReceiveDamageColdDown()
    {
        yield return new WaitForSeconds(0.4f);
        cannotBeAttacked = false;

    }
    IEnumerator Dizzy()
    {
        yield return new WaitForSeconds(0.2f);
        InDizzy = false;

    }


    public void RecoverHP(int HpPoint)
    {
        Hp = Hp + HpPoint;
        if (Hp > MaxHp)
        {
            Hp = MaxHp;
        }
    }

    public void RecoverArmor()
    {
        Armor ++;
        if (Armor > MaxArmor)
        {
            Armor = MaxArmor;
        }
    }






}


