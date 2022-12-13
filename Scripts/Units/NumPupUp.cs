using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NumPupUp : MonoBehaviour
{
    [SerializeField] Behavior player;
    [SerializeField] TMP_Text RecoverhpNumber; 
    [SerializeField] TMP_Text RecoverArmorNumber;
    [SerializeField] TMP_Text LevelUpText;
    public Animator HPAnim;
    public Animator ArmorAnim;
    public Animator LevelUpAnim;
    const float MinRange = -0.5f;
    const float MaxRange = 0.5f;
    Vector3 Pos;

    public void DisplayHpReNumber(int healPoint)
    {
        for(int i = 0; i < healPoint; i++)
        {
            Pos = player.transform.position + new Vector3(Random.Range(MinRange, MaxRange), Random.Range(MinRange, MaxRange), 0);
            Instantiate(RecoverhpNumber, Pos, Quaternion.identity);
        }
    }

    public void DisplayArmorReNumber()
    {
        Pos = player.transform.position + new Vector3(Random.Range(MinRange, MaxRange), Random.Range(MinRange, MaxRange), 0);
        Instantiate(RecoverArmorNumber, Pos, Quaternion.identity);
    }

    public void DisplayLevelUp()
    {
        Pos = player.transform.position + new Vector3(Random.Range(MinRange, MaxRange), Random.Range(MinRange, MaxRange), 0);
        Instantiate(LevelUpText, Pos, Quaternion.identity);
    }

}
