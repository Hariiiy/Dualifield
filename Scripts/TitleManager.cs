using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    [SerializeField] GameObject CharacterPanel;
    [SerializeField] GameObject UpGradePanel;
    [SerializeField] GameObject SettingPanel;
    public void OnStartButtonClick()
    {
        SceneManager.LoadScene("Game");
    }
    public void OnQuitButtonClick()
    {
        Application.Quit();
    }
    public void OnUpgradeButtonClick()
    {
        UpGradePanel.SetActive(true);
    }
    public void OnCharacterButtonClick()
    {
        CharacterPanel.SetActive(true);
    }

    public void OnCharsButtonClick()
    {
        CharacterPanel.SetActive(true);
    }
    public void OnSettingButtonClick()
    {
        SettingPanel.SetActive(true);
    }
}
