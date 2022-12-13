using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EXPSystem : MonoBehaviour
{
    [SerializeField] Behavior player;
    [SerializeField] Image EXPforeground;
    [SerializeField] NumPupUp levelText;
    public SkillManager skillManager;
    public int MaxExp;
    public int Level;



    public void CheckLevelUp()
    {
        if (player.Exp >= MaxExp)
        {
            LevelUp();
        }
    }


    void LevelUp()
    {
        player.Exp = 0;
        MaxExp += 25;
        Level++;
      
        skillManager.PickSkills();
        levelText.DisplayLevelUp();
    }



}
