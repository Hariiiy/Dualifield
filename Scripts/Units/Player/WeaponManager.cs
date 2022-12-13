using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    //weapon
    public Weapon currentweapon;
    //for test
    [SerializeField] int DefaultIndex = 0;
    public PlayerData playerData;
    private int weaponSelection;

    //Skill
    public bool BerserkerOn;
    public bool AutoRifleOn;
    
    public bool SwordDanceOn;


    void Start()
    {
        if(playerData.CurrentCharacter ==1 )
        {
            DefaultIndex = 0;
        }
        else if (playerData.CurrentCharacter == 2)
        {
            DefaultIndex = 2;
        }
        else if (playerData.CurrentCharacter == 3)
        {
            DefaultIndex = 4;
        }

        cleanAllWeapon();

        transform.GetChild(DefaultIndex).gameObject.SetActive(true);
        
        currentweapon = GetComponentInChildren(typeof(Weapon)) as Weapon;
        
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            cleanAllWeapon();
            if(BerserkerOn)
            {
                transform.GetChild(6).gameObject.SetActive(true);
            }
            else
            {
                if(playerData.CurrentCharacter == 1)
                {
                    transform.GetChild(0).gameObject.SetActive(true);
                }
                else if(playerData.CurrentCharacter == 2)
                {
                    transform.GetChild(2).gameObject.SetActive(true);
                }
                else if(playerData.CurrentCharacter == 3)
                {
                    transform.GetChild(4).gameObject.SetActive(true);
                }
            }
            currentweapon = GetComponentInChildren(typeof(Weapon)) as Weapon;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            cleanAllWeapon();
            if(AutoRifleOn)
            {
                transform.GetChild(7).gameObject.SetActive(true);
            }
            else
            {
                if (playerData.CurrentCharacter == 1)
                {
                    transform.GetChild(1).gameObject.SetActive(true);
                }
                else if (playerData.CurrentCharacter == 2)
                {
                    transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (playerData.CurrentCharacter == 3)
                {
                    transform.GetChild(5).gameObject.SetActive(true);
                }
            }
            currentweapon = GetComponentInChildren(typeof(Weapon)) as Weapon;
        }


    }


    void cleanAllWeapon()
    {
        for (int i = 0; i < transform.childCount; ++i)
        {
            if (transform.GetChild(i).CompareTag("Weapon"))
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}
