using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerArmorBar : MonoBehaviour
{
    [SerializeField] Behavior player;
    [SerializeField] HealthManager healthManager;
    [SerializeField] Image Armorforeground;
    [SerializeField] private float BarPosition = 0.7f;


    void Update()
    {
        transform.position = player.transform.position + new Vector3(0, BarPosition, 0);

        float ArmorRatio = (float)healthManager.Armor / healthManager.MaxArmor;
        Armorforeground.transform.localScale = new Vector3(ArmorRatio, 1, 1);

    }
}
