using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Behavior : MonoBehaviour
{
    public Animator Anim;

    //Enemy Attributes
    [SerializeField] string Name;
    public float speed;
    [SerializeField] float BaseSpeed = 2f;
    [SerializeField] float SpeedBoost = 3f;

    public float Hp = 20;
    public float MaxHP = 20;
    [SerializeField] string WeaknessType = "none";

    [SerializeField] float ATKRecharge = 1f;
    const float ValidAttackTime = 1.2f;
    public bool AttackVaild;
    [SerializeField] float ATKColdDown = 4f;
    float attackTimer;
    bool IsAttacking;
    int scaleX;
    GameManager gameManager;
    GameObject gameManagerObject;





    public DetectPlayer detectplayer;
    public DamagePupup damagePupup;
    public bool BodyBombOn;
    [SerializeField] GameObject Bomb;
    
    GameObject player;
 

    //receiveDamage
    bool cannotBeAttacked;
    public bool InDizzy;

    //Loot
    public int GoldNum;
    public int ExpNum;
    [SerializeField] GameObject ExpBall;
    [SerializeField] GameObject Gold;

    [SerializeField] GameObject HpPotion;
    [SerializeField] GameObject ArmorPotion;
    [SerializeField] GameObject HpPotionPlus;
    [SerializeField] GameObject BulletCase;

    float dispersion = 1f;


    //Skill
    public bool ArmorMerchantOn;
    public bool NecromancerOn;


    //Sound
    [SerializeField] GameObject soundPlayer;



    bool HalfHealthSpeed;

 

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        attackTimer = 0;
        IsAttacking = false;
        AttackVaild = false;
        speed = BaseSpeed;
        cannotBeAttacked = false;
        gameManagerObject = GameObject.FindGameObjectWithTag("GameManager");
        gameManager = gameManagerObject.GetComponent<GameManager>();
    }

    void Update()
    {
        Movement();
        if(InDizzy)
        {
            detectplayer.ReadyToAttack = false;
            IsAttacking = false;
        }
        else
        {
            Attack();
        }
    }



    public void Movement()
    {
        Vector3 destination = player.transform.position;
        Vector3 source = transform.position;

        Vector3 direction = destination - source;
        direction.Normalize();

        transform.position += direction * speed * Time.deltaTime;
        transform.localScale = new Vector3(direction.x > 0 ? -1 : 1, 1, 1);

        if (direction.x > 0)
        {
            scaleX = -1;
        }
        else if (direction.x < 0)
        {
            scaleX = 1;
        }
        else
        {
            scaleX = 0;
        }
        transform.localScale = new Vector3(scaleX, 1, 1);

        if(!IsAttacking &&!InDizzy)
        {
            Anim.Play("Walk");
        }

    }

    public void Attack()
    {
        if(detectplayer.ReadyToAttack)
        {
            IsAttacking = true;
        }



        if (IsAttacking)
        {
            attackTimer += Time.deltaTime;
        }
        else
        {
            attackTimer = 0;
        }

        if (attackTimer <= ATKRecharge && IsAttacking)
        {
            speed = SpeedBoost;
            Anim.Play("Attack");
        }
        else if (attackTimer >= ATKRecharge && attackTimer <= ValidAttackTime)
        {           
            AttackVaild = true;
            speed = BaseSpeed;
            
        }
        else if (attackTimer >ValidAttackTime&&attackTimer<=ATKColdDown)
        {
            AttackVaild = false;
            speed = 0;
        }
        else if (attackTimer>=ATKColdDown)
        {
            IsAttacking = false;
            attackTimer = 0f;
            speed = BaseSpeed;
            detectplayer.ReadyToAttack = false;
        }
        

    }

    public void ReceiveDamage(float TakenDamage,string DamageType, float AttackSpeed)
    {
        Anim.Play("Injuried");
        if (!cannotBeAttacked)
        {
            if (WeaknessType == "none")
            {
                this.Hp -= TakenDamage;
            }
            else if (WeaknessType != DamageType)
            {
                this.Hp -= TakenDamage * 0.5f;
            }
            else
            {
                this.Hp -= TakenDamage * 1.5f;
            }
            damagePupup.DisplayDamageNumber(TakenDamage);
            InDizzy = true;
            StartCoroutine(ReceiveDamageColdDown());
            StartCoroutine(Dizzy(AttackSpeed));
            Instantiate(soundPlayer, transform.position, Quaternion.identity);
        }
        cannotBeAttacked = true;
        speed = 0;
        CheckIfDead();


        if(Hp < 0.5f * MaxHP && !HalfHealthSpeed)
        {
            speed = 2 * speed;
            HalfHealthSpeed = true;
        }


    }//attacked by player

    public void ReceiveDamage(float TakenDamage)
    {
        Anim.Play("Injuried");
        if (!cannotBeAttacked)
        {
            this.Hp -= TakenDamage;
            damagePupup.DisplayDamageNumber(TakenDamage);
            InDizzy = true;
            StartCoroutine(ReceiveDamageColdDown());
            StartCoroutine(Dizzy(0.1f));
        }
        cannotBeAttacked = true;
        speed = 0;
        CheckIfDead();
    }

 

    public void CheckIfDead()
    {        
        if (this.Hp <= 0)
        {
            if (BodyBombOn)
            {
                int chanse = Random.Range(1, 100);
                if (chanse < 50)
                {
                    Vector3 BombPos = transform.position;
                    Instantiate(Bomb, BombPos, Quaternion.identity);
                }
            }
            Destroy(gameObject);
            Vector3 lootLocation = transform.position;
            GenerateExp(lootLocation);
            GenerateGold(lootLocation);
            GenerateArmorPotion(lootLocation);
            GenerateHpPotion(lootLocation);
            GenerateBulletCase(lootLocation);
            gameManager.KillCount++;
            
        }
    }


    IEnumerator ReceiveDamageColdDown()
    {
        yield return new WaitForSeconds(0.2f);
        cannotBeAttacked = false;
    }

    IEnumerator Dizzy(float attackSpeed)
    {
        yield return new WaitForSeconds(attackSpeed);
        speed = BaseSpeed;
        InDizzy = false;
    }


    //Loot
    public void GenerateExp(Vector3 location)
    {
        for (int i = 0; i <= ExpNum; i++)
        {
            location = randomGenerateCircle(location);
            Instantiate(ExpBall, location, Quaternion.identity);
        }
    }

    public void GenerateGold(Vector3 location)
    {
        for (int i = 0; i <= GoldNum; i++)
        {
            location = randomGenerateCircle(location);
            Instantiate(Gold, location, Quaternion.identity);
        }
    }

    public void GenerateHpPotion(Vector3 location)
    {
        int chanse = Random.Range(1, 100);

        int chanseTarget = 10;
        if (NecromancerOn)
        {
            chanseTarget = 20;
        }

        if (chanse <= chanseTarget)
        {
            if(chanse <= 30)
            {
                location = randomGenerateCircle(location);
                Instantiate(HpPotionPlus, location, Quaternion.identity);
            }
            else
            {
                location = randomGenerateCircle(location);
                Instantiate(HpPotion, location, Quaternion.identity);
            }
        }
    }

    public void GenerateArmorPotion(Vector3 location)
    {
        int chanse = Random.Range(1, 100);
        int chanseTarget = 15;
        if(ArmorMerchantOn)
        {
            chanseTarget = 30;
        }
        if (chanse <= chanseTarget)
        {
            location = randomGenerateCircle(location);
            Instantiate(ArmorPotion, location, Quaternion.identity);
        }
    }
    public void GenerateBulletCase(Vector3 location)
    {
        int chanse = Random.Range(1, 100);
        if (chanse <= 5)
        {
            location = randomGenerateCircle(location);
            Instantiate(BulletCase, location, Quaternion.identity);
        }
    }



    private Vector3 randomGenerateCircle(Vector3 loaction)
    {
        Vector3 GeneratePosition = loaction;
        float Gdispersion = Random.Range(-dispersion, dispersion);
        GeneratePosition.x += Gdispersion;
        Gdispersion = Random.Range(-dispersion, dispersion);
        GeneratePosition.y += Gdispersion;

        return GeneratePosition;
    }





}
