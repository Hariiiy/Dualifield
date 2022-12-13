using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EXPBar : MonoBehaviour
{
    [SerializeField] Behavior player;
    [SerializeField] EXPSystem EXPSystem;
    [SerializeField] Image EXPforeground;


    void Update()
    {
        float EXPRatio = (float)player.Exp / EXPSystem.MaxExp;
        EXPforeground.transform.localScale = new Vector3(EXPRatio, 1, 1);

    }
}
