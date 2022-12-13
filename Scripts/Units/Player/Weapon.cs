using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    //Weapon
    //If Attack valid check
    public Behavior Player;


    //Weapon Attributes
    public string WeaponName;
    public string WeaponType;
    public float Damage;
    public float AttackSpeed;
    public string DamageType;


    public GameObject bullet;
    public Transform bulletTrans;

    [SerializeField] PlayerData playerData;

    //Gun
    private Camera mainCam;
    private Vector3 mousePos;
    //Message
    [SerializeField] private Animator NoAmmoTextDisplayer;
    [SerializeField] private GameObject NoAmmoText;
    


    //Skill Loading
    public bool DoubleLoadingOn;
    public bool TribleLoadingOn;

    //Sound
    [SerializeField] GameObject weaponSound;


    void Start()
    {
        Player.BulletQuantity = 30 + playerData.CurrentBuffBulletNumLevel*3;
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        NoAmmoText.SetActive(false);
        Damage += playerData.CurrentBuffDamageLevel * 2;
    }

    void Update()
    {
        if(WeaponType == "Ranger")
        {
            RangerWeaponRotate();

            if (Player.shooting == true && Player.BulletQuantity > 0)
            {
                Instantiate(bullet, bulletTrans.position, Quaternion.identity);
                Player.BulletQuantity--;
                if(TribleLoadingOn && Player.BulletQuantity > 2)
                {
                    Instantiate(bullet, bulletTrans.position, Quaternion.identity);
                    Instantiate(bullet, bulletTrans.position, Quaternion.identity);
                    Player.BulletQuantity -= 2;
                }
                else if (DoubleLoadingOn && Player.BulletQuantity > 1)
                {
                    Instantiate(bullet, bulletTrans.position, Quaternion.identity);
                    Player.BulletQuantity--;
                }
                Instantiate(weaponSound, bulletTrans.position, Quaternion.identity);

            }
            else if(Player.shooting == true && Player.BulletQuantity == 0)
            {
                NoAmmoTextDisplayer.Play("NoAmmoDisplay");
                NoAmmoText.SetActive(true);
                StartCoroutine(DisActiveTextCoroutine());
            }
            Player.shooting = false;
        }
    }

    IEnumerator DisActiveTextCoroutine()
    {
        yield return new WaitForSeconds(2);
        NoAmmoText.SetActive(false);
    }



    //melee
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(WeaponType == "Melee")
        {
            GameObject other = collision.gameObject;

            if (Player.AttackVaild && other.CompareTag("Enemy"))
            {
                other.GetComponent<Enemy_Behavior>().ReceiveDamage(Damage, DamageType, AttackSpeed);
            }
        }
    }

    private void RangerWeaponRotate()
    {
        //make weapon rotate base one the mouse position
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePos - transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ - 90);
    }
}
