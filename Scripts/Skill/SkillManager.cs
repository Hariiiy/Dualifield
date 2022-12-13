using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public GameObject levelUpMenu;
    public GameObject skill1;
    public GameObject skill2;
    public GameObject skill3;
    public List<GameObject> Skills = new List<GameObject>();
    [SerializeField] PauseManager pauseManager;
    bool noRepetition;
    public bool SkillButtonAvailable;

    [SerializeField] GameObject SkillPlace1;
    [SerializeField] GameObject SkillPlace2;
    [SerializeField] GameObject SkillPlace3;


    //Skills
    public bool BlastShotOn;
    public bool swordDanceOn;
    public bool ExplosionArtistOn;
    public bool BodyBombOn;



    [SerializeField] private Bullet bullet;
    [SerializeField] private Weapon pistol;
    [SerializeField] private Weapon Auto;
    [SerializeField] private Weapon AR;
    [SerializeField] private Weapon LM;

    [SerializeField] private Behavior player;
    [SerializeField] private HealthManager playerHealthSys;
    
    
    [SerializeField] private Enemy_Behavior walker;
    [SerializeField] private Enemy_Behavior runner;
    //...


    [SerializeField] private WeaponManager weaponManager;
    [SerializeField] private SwordDance swordDance;




    void Start()
    {
        levelUpMenu.SetActive(false);
        SkillButtonAvailable = true; //actully not using this
        cleanAllSkill();
        BuffReset();
      
    }


    void cleanAllSkill()
    {
        for (int i = 0; i < Skills.Count; ++i)
        {
            Skills[i].SetActive(false);
        }
    }

    public void PickSkills()
    {
        levelUpMenu.SetActive(true);
        cleanAllSkill();
        while(!noRepetition)
        {            
            int selection1 = Random.Range(0, Skills.Count);
            int selection2 = Random.Range(0, Skills.Count);
            int selection3 = Random.Range(0, Skills.Count);
            skill1 = Skills[selection1];
            skill2 = Skills[selection2];
            skill3 = Skills[selection3];
            if (skill1 != skill2 && skill2 != skill3 && skill1 != skill3)
            {
                noRepetition = true;
            }
        }
        noRepetition = false;

        Vector3 place1 = SkillPlace1.transform.position;
        Vector3 place2 = SkillPlace2.transform.position;
        Vector3 place3 = SkillPlace3.transform.position;

        skill1.transform.position = place1;
        skill2.transform.position = place2;
        skill3.transform.position = place3;

        skill1.SetActive(true);
        skill2.SetActive(true);
        skill3.SetActive(true);
        pauseManager.PauseGame();
    }




    public void OnArmorReButtonClick()
    {
        if(SkillButtonAvailable)
        {
            player.ArmorReOn = true;
            ExitSkillMenu();
        }

    }
    public void OnHellFireButtonClick()
    {
        if (SkillButtonAvailable)
        {
            bullet.HellFireOn = true;
            ExitSkillMenu();
        }

    }
    public void OnBlastShotButtonClick()
    {
        if (SkillButtonAvailable)
        {
            bullet.BlastShotOn = true;
            ExitSkillMenu();
        }

    }
    public void OnEnhancedAmmoButtonClick()
    {
        if (SkillButtonAvailable)
        {
            bullet.EnhancedAmmoOn = true;
            ExitSkillMenu();
        }

    }
    public void OnDoubleLoadingButtonClick()
    {
        if (SkillButtonAvailable)
        {
            LM.DoubleLoadingOn = true;
            pistol.DoubleLoadingOn = true;
            AR.DoubleLoadingOn = true;
            ExitSkillMenu();
        }

    }
    public void OnTribleLoadingButtonClick()
    {
        if (SkillButtonAvailable)
        {
            LM.TribleLoadingOn = true;
            pistol.TribleLoadingOn = true;
            AR.TribleLoadingOn = true;
            ExitSkillMenu();
        }
 
    }
    public void OnHealthReButtonClick()
    {
        if (SkillButtonAvailable)
        {
            player.HealthReOn = true;
            ExitSkillMenu();
        }

    }
    public void OnArmorMerchantButtonClick()
    {
        if (SkillButtonAvailable)
        {
            walker.ArmorMerchantOn = true;
            runner.ArmorMerchantOn = true;
            ExitSkillMenu();
        }
 
    }
    public void OnNecromancerButtonClick()
    {
        if (SkillButtonAvailable)
        {
            walker.NecromancerOn = true;
            runner.NecromancerOn = true;
            ExitSkillMenu();
        }

    }
    public void OnDoubleBenefitButtonClick()
    {
        if (SkillButtonAvailable)
        {
            player.DoubleBenefitOn = true;
            ExitSkillMenu();
        }

    }
    public void OnRunnerButtonClick()
    {
        if (SkillButtonAvailable)
        {
            player.RunnerOn = true;
            ExitSkillMenu();
        }

    }
    public void OnDedicatedToDeathButtonClick()
    {
        if (SkillButtonAvailable)
        {
            playerHealthSys.DedicatedToDeathOn = true;
            ExitSkillMenu();
        }

    }
    public void OnAutoRifleButtonClick()
    {
        if (SkillButtonAvailable)
        {
            weaponManager.AutoRifleOn = true;
            ExitSkillMenu();
        }

    }
    public void OnBerserkerButtonClick()
    {
        if (SkillButtonAvailable)
        {
            weaponManager.BerserkerOn = true;
            ExitSkillMenu();
        }

    }
    public void OnswordDanceButtonClick()
    {
        if (SkillButtonAvailable)
        {
            swordDance.SetSwordDanceAcitve();
            ExitSkillMenu();
        }

    }

    public void OnBodyBombButtonClick()
    {
        if (SkillButtonAvailable)
        {
            walker.BodyBombOn = true;
            runner.BodyBombOn = true;
            ExitSkillMenu();
        }

    }

    public void ExitSkillMenu()
    {
        if (SkillButtonAvailable)
        {
            levelUpMenu.SetActive(false);
            pauseManager.ResumeGame();
        }
    }


    private void BuffReset()
    {
        pistol.DoubleLoadingOn = false;
        AR.DoubleLoadingOn = false;
        LM.DoubleLoadingOn = false;
        pistol.TribleLoadingOn = false;
        AR.TribleLoadingOn = false;
        LM.TribleLoadingOn = false;
        bullet.HellFireOn = false;
        bullet.EnhancedAmmoOn = false;
        bullet.BlastShotOn = false;



        player.ArmorReOn = false;
        player.HealthReOn = false;
        walker.ArmorMerchantOn = false;
        runner.ArmorMerchantOn = false;
        walker.NecromancerOn = false;
        runner.NecromancerOn = false;
        player.DoubleBenefitOn = false;
        player.RunnerOn = false;
        playerHealthSys.DedicatedToDeathOn = false;


        weaponManager.BerserkerOn = false;
        weaponManager.AutoRifleOn = false;
        weaponManager.SwordDanceOn = false;
        walker.BodyBombOn = false;
        runner.BodyBombOn = false;
    }

}
