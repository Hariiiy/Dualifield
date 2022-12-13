using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 mousePos;
    private Camera mainCam;
    int BulletDamage;
    public float Speed;
    public float Dispersion;

    //use rigid body to push pullet
    private Rigidbody2D rb;



    //Skill HellFire
    public bool HellFireOn;
    public SpriteRenderer spriteRenderer;
    public Sprite HellFireSprite;
    public bool shootingHellFire;
    bool destoryBullet;

    //Skill EnhancedAmmo
    public bool EnhancedAmmoOn;


    public bool BlastShotOn;
    public bool shootingBlastShot;
    [SerializeField] GameObject bomb;


    [SerializeField] PlayerData playerData;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        Dispersion = 1 - (playerData.CurrentBuffAccuracyLevel * 0.15f);
        rb = GetComponent<Rigidbody2D>();
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        Vector3 direction = mousePos - transform.position;
        direction.x += Random.Range(-Dispersion, Dispersion);
        direction.y += Random.Range(-Dispersion, Dispersion);

        Vector3 rotation = transform.position - mousePos;

        rb.velocity = new Vector2(direction.x, direction.y).normalized * Speed;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot+180);
        
        BulletDamage = 12;
        BulletDamage += playerData.CurrentBuffDamageLevel * 2;

        CheckHellFire();
        CheckBlastShot();
        StartCoroutine(DestoryBulletCoroutine());

    }

    // Update is called once per frame
    void Update()
    {
        if(destoryBullet)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        GameObject other = collision.gameObject;

        if (other.CompareTag("Enemy"))
        {
            if (EnhancedAmmoOn)
            {
                BulletDamage = BulletDamage + Mathf.FloorToInt(BulletDamage * 0.5f);
            }   
            
            other.GetComponent<Enemy_Behavior>().ReceiveDamage(BulletDamage);
            

            if (shootingBlastShot)
            {
                Vector3 bombPos = transform.position;
                Instantiate(bomb, bombPos, Quaternion.identity);
            }

            if (!shootingHellFire)
            {
                Destroy(gameObject);
            }         
        }
    }

    IEnumerator DestoryBulletCoroutine()
    {
        yield return new WaitForSeconds(5f);
        destoryBullet = true;
    }


    private void CheckHellFire()
    {
        if (HellFireOn)
        {
            int chanse = Random.Range(1, 100);
            if (chanse <= 30)
            {
                spriteRenderer.sprite = HellFireSprite;
                shootingHellFire = true;
            }
            else
            {
                shootingHellFire = false;
            }
        }
    }

    private void CheckBlastShot()
    {
        if (BlastShotOn)
        {
            int chanse = Random.Range(1, 100);
            if (chanse <= 30)
            {
                shootingBlastShot = true;
            }
            else
            {
                shootingBlastShot = false;
            }
        }
    }
}
