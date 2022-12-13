using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class LightControl : MonoBehaviour
{

    [SerializeField] Light2D playerlight;
    [SerializeField] Light2D globalLight;
    [SerializeField] PlayerData playerdata;
    [SerializeField] Bloom maincameraBloom;



    void Start()
    {
        playerlight.intensity = 0.4f + (playerdata.LightEffectLevel * 0.04f);
        globalLight.intensity = 0.25f + (playerdata.LightEffectLevel * 0.02f);
        //maincameraBloom.intensity = 0.25f + (playerdata.LightEffectLevel * 0.02f);
    }

    void Update()
    {
        
    }
}
