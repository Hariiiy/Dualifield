using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePupup : MonoBehaviour
{
    [SerializeField] Enemy_Behavior enemy;
    [SerializeField] TMP_Text DamageNumber;
    public Animator Anim;
    const float MinRange = -0.5f;
    const float MaxRange = 0.5f;
    Vector3 DamageNumberPos;

    public void DisplayDamageNumber(float recivedDamage)
    {
        DamageNumberPos = enemy.transform.position + new Vector3(Random.Range(MinRange,MaxRange), Random.Range(MinRange, MaxRange), 0);
        DamageNumber.text = recivedDamage.ToString();
        Instantiate(DamageNumber, DamageNumberPos, Quaternion.identity);
    }


}

