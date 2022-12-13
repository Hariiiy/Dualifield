using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    public GameObject Enemy;
    GameObject Player;

    [SerializeField] int DetectDistance;
    [SerializeField] int VisionDistance;

    public bool ReadyToAttack;
    public bool FollowPlayer;

    void Start()
    {
        ReadyToAttack = false;
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        AttackChecker();
    }


    private void AttackChecker()
    {
        Vector3 playerPosition = Player.transform.position;
        Vector3 enemyPosition = Enemy.transform.position;

        float distance = Vector3.Distance(enemyPosition, playerPosition);

        if(distance < DetectDistance)
        {
            ReadyToAttack = true;
        }
        
    }



}
