using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Setting : MonoBehaviour
{

    [SerializeField] PlayerData playerData;
    [SerializeField] TMP_Text LightNote;
    [SerializeField] TMP_Text BloomNote;
    [SerializeField] GameObject SettingPanel;
    void Start()
    {
        SettingPanel.SetActive(false);
        LightNote.text = playerData.CurrentlightEffectLevel.ToString();
        BloomNote.text = playerData.CurrentBloomLevel.ToString();
    }
    public void OnLightUpClick()
    {

        if (playerData.CurrentlightEffectLevel < playerData.LightEffectLevel)
        {
            playerData.CurrentlightEffectLevel++;
            LightNote.text = playerData.CurrentlightEffectLevel.ToString();
        }
    }

    public void OnLightDownClick()
    {
        if (playerData.CurrentlightEffectLevel > 0)
        {
            playerData.CurrentlightEffectLevel--;
            LightNote.text = playerData.CurrentlightEffectLevel.ToString();
        }
    }

    public void OnDoneClick()
    {
        SettingPanel.SetActive(false);
    }

    public void OnBloomUpClick()
    {
        if (playerData.CurrentBloomLevel < playerData.BloomLevel)
        {
            playerData.CurrentBloomLevel++;
            BloomNote.text = playerData.CurrentBloomLevel.ToString();
        }
    }

    public void OnBloomDownClick()
    {
        if(playerData.CurrentBloomLevel>0)
        {
            playerData.CurrentBloomLevel--;
            BloomNote.text = playerData.CurrentBloomLevel.ToString();
        }
    }




}
