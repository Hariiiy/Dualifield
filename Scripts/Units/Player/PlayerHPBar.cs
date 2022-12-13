using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerHPBar : MonoBehaviour
{
    [SerializeField] Behavior player;
    [SerializeField] HealthManager healthManager;
    [SerializeField] Image HPforeground;
    [SerializeField] private float BarPosition = 0.5f;


    void Update()
    {
        transform.position = player.transform.position + new Vector3(0,BarPosition,0);

        //between 0 and 1
        float hpRatio = (float)healthManager.Hp / healthManager.MaxHp;
        HPforeground.transform.localScale = new Vector3(hpRatio, 1, 1);

    }
}
