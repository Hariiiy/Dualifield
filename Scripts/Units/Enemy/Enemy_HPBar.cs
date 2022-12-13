using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Enemy_HPBar : MonoBehaviour
{
    [SerializeField] Enemy_Behavior enemy;
    [SerializeField] Image foreground;
    [SerializeField] private float HPBarPosition = 0.7f;


    void Update()
    {
        transform.position = enemy.transform.position + new Vector3(0, HPBarPosition, 0);

        //between 0 and 1
        float hpRatio = (float)enemy.Hp / enemy.MaxHP;
        foreground.transform.localScale = new Vector3(hpRatio, 1, 1);
    }
}
