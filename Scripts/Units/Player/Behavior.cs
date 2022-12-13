using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Behavior : MonoBehaviour
{
    public Animator Anim;

    public HealthManager healthManager;
    public WeaponManager weaponManager;
    public EXPSystem EXP;
    [SerializeField] PlayerData playerData;


    //player movement
    private float shiftStart;
    private float shiftHoldTime;
    [SerializeField] float speed = 2f;
    [SerializeField] float runingSpeed = 3f;
    [SerializeField] float superRuningSpeed = 4f;
    [SerializeField] float dashingSpeed = 6f;
    [SerializeField] float superDashingSpeed = 8f;
    private bool runing;
    private bool dashing;
    private float walkSpeed;
    private float dashCheckLine;
    private float boostTimer;
    private float dashLimit;

    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite survivorSprite;
    [SerializeField] Sprite hackerSprite;
    [SerializeField] Sprite darkSprite;



    //Dodge
    Rigidbody2D PlayerRigidbody;

    //position check (WalkAnim)
    Vector3 targetVel;
    Vector3 lastVel;

    //Attack
    private float attackTimer;
    private float attackVaildTimer;
    private bool isAttacking;
    public bool AttackVaild;
    public bool shooting;


    //player turn
    public Vector3 mousePos;
    public float mouseX;


    bool GoToEndingMenu;
    const float EndingDelayTime = 5f;

    //Loot
    public int Exp = 0;
    public int Gold = 0;
    public int BulletQuantity;



    public NumPupUp PupUp;



    //Skill
    public bool ArmorReOn;
    public bool HealthReOn;
    public bool DoubleBenefitOn;
    public bool RunnerOn;

    bool HPRecoverColddown;
    bool ArmorRecoverColddown;

    GameManager gameManager;
    GameObject gameManagerObject;

    void Start()
    {
        //player movement
        dashCheckLine = 0.2f;
        dashLimit = 0.3f;
        runing = false;
        dashing = false;
        walkSpeed = speed;

        targetVel = this.transform.position;

        //attack
        AttackVaild = false;
        shooting = false;
        PlayerRigidbody = GetComponent<Rigidbody2D>();

        BulletQuantity = 12 + playerData.CurrentBuffBulletNumLevel * 3;
        HPRecoverColddown = false;
        ArmorRecoverColddown = false;
        StartCoroutine(HPRecoverCoroutine());
        StartCoroutine(ArmorRecoverCoroutine());

        Gold = playerData.PlayerDataGold;

        if(playerData.CurrentCharacter == 1)
        {
            spriteRenderer.sprite = survivorSprite;
        }
        else if(playerData.CurrentCharacter == 2)
        {
            spriteRenderer.sprite = hackerSprite;
        }
        else if(playerData.CurrentCharacter == 3)
        {
            spriteRenderer.sprite = darkSprite;
        }

        if(playerData.CurrentCharacter == 3)
        {
            speed = 3f;
            runingSpeed = 4f;
            superRuningSpeed =5f;
        }

        gameManagerObject = GameObject.FindGameObjectWithTag("GameManager");
        gameManager = gameManagerObject.GetComponent<GameManager>();
    }


    void Update()
    {
        if(healthManager.PlayerDead)
        {
            Anim.Play("Dead");
            StartCoroutine(EndingCoroutine());
            gameManager.DisplayDeathScreeninfo();
        }
        else if (healthManager.InDizzy)
        {
            Anim.Play("Injuried");
        }
        else
        {
            Attack();
            PlayerMove();
        }   

        if(GoToEndingMenu)
        {
            gameManager.CloseDeathScreeninfo();
            SceneManager.LoadScene("GameOver");
        }

        if(ArmorReOn||HealthReOn)
        {           
            if(ArmorReOn && ArmorRecoverColddown)
            {
                armorRe();
                ArmorRecoverColddown=false;
                StartCoroutine(HPRecoverCoroutine());
            }
            if(HealthReOn && HPRecoverColddown)
            {
                healthRe();
                HPRecoverColddown=false;
                StartCoroutine(ArmorRecoverCoroutine());
            }
        }
    }

    //walking check
    private void PlayerMove()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");
        lastVel = this.transform.position;

        //run and dash
        //check the key press time to separate "run" and "dash"
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (runing == false || dashing == false)
            {
                shiftStart = Time.time;//count start
                runing = true;
            }
        }

        //count the time between press and release
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            shiftHoldTime = Time.time - shiftStart;//count end

            if (shiftHoldTime <= dashCheckLine)
            {
                dashing = true;
            }
        }

        //if just press key for short time = dash
        if (dashing && isAttacking == false)
        {
            if(RunnerOn)
            {
                speed = superDashingSpeed;
            }
            else
            {
                speed = dashingSpeed;
            }
            boostTimer += Time.deltaTime;
            Anim.Play("Dodge");
            PlayerRigidbody.mass = 10;
            if (boostTimer > dashLimit)//||isAttacking
            {
                speed = walkSpeed;
                boostTimer = 0;
                dashing = false;
                runing = false;
                PlayerRigidbody.mass = 1;
            }
        }

        //hold the key = run
        if (runing && !isAttacking)
        {
            if(RunnerOn)
            {
                speed = superRuningSpeed;
            }
            else
            {
                speed = runingSpeed;
            }
            
            //animation
            if (MovingCheck())//if player hold shift but character do not change position, play idle animation
            {
                Anim.Play("Run");
            }
            if (!MovingCheck())
            {
                Anim.Play("Idle");
            }

            if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.Mouse0)) //if player release shift or doing attack, put player back to walking speed
            {
                speed = walkSpeed;
                runing = false;
            }

        }


        //play walking animation (if player moving without dash and run, player walk animation)
        if (MovingCheck() && !dashing && !runing && !isAttacking)
        {
            Anim.Play("Walk");
        }
        if (!MovingCheck() && !dashing && !runing && !isAttacking )
        {
            Anim.Play("Idle");
        }

        //player turn (follow the mouse)
        transform.localScale = new Vector3(ReturnscaleX(), 1, 1);  

        //position change
        transform.position += new Vector3(inputX, inputY, 0) * speed * Time.deltaTime;
        targetVel = lastVel;
    }

    private bool MovingCheck()
    {
        if (targetVel != lastVel)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private int ReturnscaleX()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseX = mousePos.x;

        if (mouseX < transform.position.x)
        {
            return -1;
        }
        else if (mouseX > transform.position.x)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    //Heavy Weapon Attack (light and range on progress)
    private void Attack() 
    {
        if(weaponManager.currentweapon.WeaponType == "Melee")
        {
            //Melle Attack     
            if (isAttacking)
            {
                attackVaildTimer += Time.deltaTime;
            }

            if (attackTimer <= weaponManager.currentweapon.AttackSpeed)
            {
                attackTimer += Time.deltaTime;
            }
            else if (attackTimer >= weaponManager.currentweapon.AttackSpeed)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0) && dashing == false)
                {
                    Anim.Play(weaponManager.currentweapon.WeaponName);
                    attackTimer = 0;
                    isAttacking = true;
                    AttackVaild = true;
                    attackVaildTimer = 0;
                }
                else if (attackTimer > weaponManager.currentweapon.AttackSpeed)
                {
                    isAttacking = false;
                }
            }

            if (attackVaildTimer >= 0.15f)
            {
                AttackVaild = false;
            }
        }
        
        if (weaponManager.currentweapon.WeaponType == "Ranger")
        {
            if (attackTimer <= weaponManager.currentweapon.AttackSpeed)
            {
                attackTimer += Time.deltaTime;
            }
            
            if (attackTimer >= weaponManager.currentweapon.AttackSpeed)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0) && dashing == false)
                {
                    attackTimer = 0;
                    shooting = true;
                    isAttacking = true;
                }
                else if (attackTimer > weaponManager.currentweapon.AttackSpeed)
                {
                    isAttacking = false;
                }
            }
        }
    }


    IEnumerator EndingCoroutine()
    {
        yield return new WaitForSeconds(EndingDelayTime);
        GoToEndingMenu = true;
    }


    //pickup
    private void OnTriggerStay2D(Collider2D collision)
    {

        GameObject other = collision.gameObject;

        if (other.CompareTag("Gold"))
        {
            if(DoubleBenefitOn)
            {
                Gold += 2;
                gameManager.goldCount += 2;
            }
            else
            {
                Gold += 1;
                gameManager.goldCount += 1;
            }
            Destroy(other);
        }

        if (other.CompareTag("Exp"))
        {
            Exp += 1;
            gameManager.expCount += 1;
            EXP.CheckLevelUp();
            Destroy(other);
        }

        if (other.CompareTag("HpPotion") && healthManager.Hp < healthManager.MaxHp)
        {
            int HealPoint = Mathf.FloorToInt(healthManager.MaxHp * 0.25f);
            healthManager.RecoverHP(HealPoint);
            Destroy(other);
            PupUp.DisplayHpReNumber(HealPoint);
        }

        if (other.CompareTag("HpPotionPlus") && healthManager.Hp < healthManager.MaxHp)
        {
            int HealPoint = Mathf.FloorToInt(healthManager.MaxHp * 0.5f);
            healthManager.RecoverHP(HealPoint);
            Destroy(other);
            PupUp.DisplayHpReNumber(HealPoint);
        }

        if (other.CompareTag("ArmorPotion") && healthManager.Armor < healthManager.MaxArmor)
        {
            healthManager.RecoverArmor();
            Destroy(other);
            PupUp.DisplayArmorReNumber();
        }

        if (other.CompareTag("BulletCase"))
        {
            int bulletInCase = Random.Range(1, 12);
            BulletQuantity += bulletInCase;
            Destroy(other);
        }

    }

    private void armorRe()
    {
        healthManager.RecoverHP(1);
        PupUp.DisplayHpReNumber(1);
    }

    private void healthRe()
    {
        healthManager.RecoverArmor();
        PupUp.DisplayArmorReNumber();
    }

    IEnumerator HPRecoverCoroutine()
    {
        yield return new WaitForSeconds(30);
        HPRecoverColddown = true;
    }

    IEnumerator ArmorRecoverCoroutine()
    {
        yield return new WaitForSeconds(30);
        ArmorRecoverColddown = true;
    }

    public void SavePlayerGoldToData()
    {
        playerData.PlayerDataGold += Gold;
    }
}