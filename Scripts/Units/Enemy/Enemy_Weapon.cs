using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Weapon : MonoBehaviour
{
    public Enemy_Behavior Self;


    private void OnTriggerStay2D(Collider2D collision)
    {
        GameObject other = collision.gameObject;
        if (other.CompareTag("Player") && Self.AttackVaild)
        {
            HealthManager target = other.GetComponent<HealthManager>();
            if(!target.PlayerDead)
            {
                target.ReceiveDamage();
            }
        }
    }
}
