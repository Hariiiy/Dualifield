using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingMenuManager : MonoBehaviour
{
    bool showButton = false;
    float countDownTimer = 1.5f;
    [SerializeField] GameObject button1;
    [SerializeField] GameObject button2;

    private void Start()
    {
        button1.SetActive(false);
        button2.SetActive(false);
        StartCoroutine(ShowButtonDelate());
    }

    private void Update()
    {
        if(showButton)
        {
            button1.SetActive(true);
            button2.SetActive(true);
        }
    }

    public void OnReStartButtonClick()
    {
        SceneManager.LoadScene("Title");
    }
    public void OnQuitButtonClick()
    {
        Application.Quit();
    }

    IEnumerator ShowButtonDelate()
    {
        yield return new WaitForSeconds(countDownTimer);
       showButton = true;
    }
}
