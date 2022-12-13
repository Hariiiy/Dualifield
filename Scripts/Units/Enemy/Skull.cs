using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skull : MonoBehaviour
{

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


    GameObject player;


    //receiveDamage
    bool cannotBeAttacked;
    public bool InDizzy;

    //Loot
    public int GoldNum;
    public int ExpNum;


    float dispersion = 1f;


    //Skill
    public bool ArmorMerchantOn;
    public bool NecromancerOn;


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
        StartCoroutine(selfDestory());
    }

    void Update()
    {
        Movement();

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
    }

    IEnumerator selfDestory()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        GameObject other = collision.gameObject;
        if (other.CompareTag("Player"))
        {
            HealthManager target = other.GetComponent<HealthManager>();
            if (!target.PlayerDead)
            {
                target.ReceiveDamage();
                Destroy(gameObject);
            }
        }
    }
}
