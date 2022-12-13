using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestBuff : MonoBehaviour
{
    [SerializeField] PlayerData playerData;
    private void Awake()
    {
        playerData.CurrentBuffAccuracyLevel = 0;
        playerData.CurrentBuffArmorLevel = 0;
        playerData.CurrentBuffBulletNumLevel = 0;
        playerData.CurrentBuffDamageLevel = 0;
        playerData.CurrentBuffHPLevel = 0;

        playerData.MaxBuffHPLevel = 10;
        playerData.MaxBuffBulletNumLevel = 10;
        playerData.MaxBuffArmorLevel = 10;
        playerData.MaxBuffAccuracyLevel = 10;
        playerData.MaxBuffArmorLevel = 10;
    }
}
