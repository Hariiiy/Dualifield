using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Behavior playerInfo;
    [SerializeField] Enemy_Behavior enemy;
    [SerializeField] EXPSystem EXP;
    [SerializeField] GameObject Walker; 
    [SerializeField] GameObject Runner;
    [SerializeField] GameObject Walker_Boss;
    [SerializeField] GameObject Runner_Boss;
    [SerializeField] GameObject Bloody;
    [SerializeField] GameObject Boss;

    //UI
    [SerializeField] TMP_Text timerText;
    [SerializeField] TMP_Text GoldNumText;
    [SerializeField] TMP_Text BulletNumText;
    [SerializeField] TMP_Text PlayerLvText;


    public int KillCount;
    public int goldCount;
    public int expCount;
    
    [SerializeField] TMP_Text KillCountText;
    [SerializeField] TMP_Text goldCountText;
    [SerializeField] TMP_Text expCountText;
    [SerializeField] GameObject DeadScreen;


    //Spawn
    public bool SpawnAvailable;
    public bool SpawnWall;
    [SerializeField] float spawnDistance = 40f;
    [SerializeField] float spawnSpeed = 12f;


    //Timer
    float seconds;
    string displayMins = "00";
    string displaySecs = "00";//for display 



    private void Start()
    {
        SpawnAvailable = true;
        StartCoroutine(SpawnPattern_Normal());
        StartCoroutine(SpawnPattern_Bloody());
        StartCoroutine(SpawnPattern_Boss());
        StartCoroutine(TeleportPlayer());
        
        transform.Translate(Vector3.back);

        DeadScreen.SetActive(false);
    }

    private void Update()
    {

        DisplayTimer();
        DisplayGoldNum();
        DisplayPlayerLv();
        DisplayBulletNum();
        DisplayGoldInfo();
        DisplayEXPInfo();
        DisplayKillInfo();

    }

    //Spawn
    private void SpawnSmallTide(int spawnNumber)
    {
        for(int i = 0; i < spawnNumber; i++)
        {
            SpawnCorpseCircle();
        }
        SpawnAvailable = false;
    }

    private void SpawnBloodyTide(int spawnNumber)
    {
        for (int i = 0; i < spawnNumber; i++)
        {
            SpawnBloodyCircle();
        }
        SpawnAvailable = false;
    }


    private void SpawnCorpseCircle()
    {
        Vector3 spawnPosition = new Vector3(145, 0, 0);
        Vector2 spawnCircle = Random.insideUnitCircle;
        spawnCircle.Normalize();
        spawnCircle *= spawnDistance;

        spawnPosition.x = spawnCircle.x;
        spawnPosition.y = spawnCircle.y;

        int randomMonster = Random.Range(1, 3);

        if(randomMonster == 1)
        {
            randomMonster = Random.Range(1, 20);
            if(randomMonster > 18)
            {
                Instantiate(Walker_Boss, spawnPosition, Quaternion.identity);
            }
            else
            {
                Instantiate(Walker, spawnPosition, Quaternion.identity);
            }
        }
        if(randomMonster == 2)
        {
            randomMonster = Random.Range(1, 20);
            if (randomMonster > 18)
            {
                Instantiate(Runner_Boss, spawnPosition, Quaternion.identity);
            }
            else
            {
                Instantiate(Runner, spawnPosition, Quaternion.identity);
            }
                
        }
    }

    private void SpawnBloodyCircle()
    {
        Vector3 spawnPosition = new Vector3(145, 0, 0);
        Vector2 spawnCircle = Random.insideUnitCircle;
        spawnCircle.Normalize();
        spawnCircle *= spawnDistance;

        spawnPosition.x = spawnCircle.x + 145;
        spawnPosition.y = spawnCircle.y;
        
        Instantiate(Bloody, spawnPosition, Quaternion.identity);
    }


    private void SpawnBoss()
    {
        Vector3 spawnPosition = new Vector3(145, 0, 0);
        Vector2 spawnCircle = Random.insideUnitCircle;
        spawnCircle.Normalize();
        spawnCircle *= spawnDistance;

        spawnPosition.x = spawnCircle.x + 145;
        spawnPosition.y = spawnCircle.y;

        Instantiate(Boss, spawnPosition, Quaternion.identity);
    }



    private void SpawnCorpseWall(int numberOfSpawn)
    {
        Vector3 spawnPosition = player.transform.position;
        spawnPosition.x += spawnDistance;
        spawnPosition.y -= numberOfSpawn;

        for (int i = 0; i < numberOfSpawn; i++)
        {
            spawnPosition.y += 2;
            int chanes = Random.Range(1,100);
            if(chanes <=50)
            {
                Instantiate(Walker, spawnPosition, Quaternion.identity);
            }
            else
            {
                Instantiate(Runner, spawnPosition, Quaternion.identity);
            }            
        }
        SpawnWall = false;
    }
    private void SpawnBloodyWall(int numberOfSpawn)
    {
        Vector3 spawnPosition = new Vector3(145, 0, 0);
        spawnPosition.x += spawnDistance + 145;
        spawnPosition.y -= numberOfSpawn;

        for (int i = 0; i < numberOfSpawn; i++)
        {
            spawnPosition.y += 2;

            Instantiate(Bloody, spawnPosition, Quaternion.identity);
        }
        SpawnWall = false;
    }



    IEnumerator SpawnPattern_Normal()
    {
        yield return new WaitForSeconds(0f);
        int SpawnUnit = 2 + (EXP.Level + 1) * 2;
        SpawnSmallTide(SpawnUnit);

        yield return new WaitForSeconds(10f);
        SpawnUnit = 2 + (EXP.Level + 1) * 2;
        SpawnSmallTide(SpawnUnit);

        yield return new WaitForSeconds(10f);
        SpawnUnit = 2 + (EXP.Level + 1) * 2;
        SpawnSmallTide(SpawnUnit);

        yield return new WaitForSeconds(10f);
        SpawnUnit = 10 + EXP.Level * 1;
        SpawnCorpseWall(SpawnUnit);

        yield return new WaitForSeconds(10f);
        SpawnUnit = 2 + (EXP.Level + 1) * 2;
        SpawnSmallTide(SpawnUnit);

        yield return new WaitForSeconds(10f);
        SpawnUnit = 2 + (EXP.Level + 1) * 2;
        SpawnSmallTide(SpawnUnit);

        yield return new WaitForSeconds(10f);
        SpawnUnit = 10 + EXP.Level * 1;
        SpawnCorpseWall(SpawnUnit);

    }

    IEnumerator SpawnPattern_Bloody()
    {
        yield return new WaitForSeconds(70f);
        int SpawnUnit = 2 + (EXP.Level + 1) * 2;
        SpawnBloodyTide(SpawnUnit);

        yield return new WaitForSeconds(10f);
        SpawnUnit = 2 + (EXP.Level + 1) * 2;
        SpawnBloodyTide(SpawnUnit);

        yield return new WaitForSeconds(10f);
        SpawnUnit = 2 + (EXP.Level + 1) * 2;
        SpawnBloodyTide(SpawnUnit);

        yield return new WaitForSeconds(10f);
        SpawnUnit = 10 + EXP.Level * 2;
        SpawnBloodyWall(SpawnUnit);

        yield return new WaitForSeconds(10f);
        SpawnUnit = 2 + (EXP.Level + 1) * 3;
        SpawnBloodyTide(SpawnUnit);

        yield return new WaitForSeconds(10f);
        SpawnUnit = 2 + (EXP.Level + 1) * 3;
        SpawnBloodyTide(SpawnUnit);

        yield return new WaitForSeconds(10f);
        SpawnUnit = 2 + (EXP.Level + 1) * 3;
        SpawnBloodyTide(SpawnUnit);

    }

    IEnumerator SpawnPattern_Boss()
    {
        yield return new WaitForSeconds(150f);
        SpawnBoss();
    }


    IEnumerator TeleportPlayer()
    {
        yield return new WaitForSeconds(70f);
        player.transform.position = new Vector3(145,0,0);
    }



    //Timer
    private void DisplayTimer()
    {
        seconds += Time.deltaTime;

        float mins = Mathf.Floor(seconds / 60); //to secs
        float secs = Mathf.RoundToInt(seconds % 60); //to mins

        //adding 0 if a number less than 10; so it will display as ex: 01:09
        if (mins < 10)
        { 
            displayMins = "0" + mins.ToString();
        }
        else 
        { 
            displayMins = mins.ToString();
        }

        if (secs < 10) 
        { 
            displaySecs = "0" + secs.ToString(); 
        }
        else 
        {
            displaySecs = secs.ToString(); 
        }

        timerText.text = displayMins + ":" + displaySecs;
    }

    private void DisplayGoldNum()
    {
        GoldNumText.text = playerInfo.Gold.ToString();
    }

    private void DisplayBulletNum()
    {
        BulletNumText.text = playerInfo.BulletQuantity.ToString();
    }

    private void DisplayPlayerLv()
    {
        PlayerLvText.text = EXP.Level.ToString();
    }


    private void DisplayGoldInfo()
    {
        goldCountText.text = goldCount.ToString();
    }

    private void DisplayKillInfo()
    {
        KillCountText.text = KillCount.ToString();
    }

    private void DisplayEXPInfo()
    {
        expCountText.text = expCount.ToString();
    }

    public void DisplayDeathScreeninfo()
    {
        DeadScreen.SetActive(true);
    }

    public void CloseDeathScreeninfo()
    {
        DeadScreen.SetActive(false);
    }
}
