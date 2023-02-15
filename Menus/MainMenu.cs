using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public GameObject mainmenu;
    public GameObject levelSelect;
    public GameObject options;
    public AudioSource playUi;

    private void Awake()
    {
        Time.timeScale = 1f;
    }

    public void OnClickStartGame()
    {
        SceneManager.LoadScene(1);
        playUi.Play();    
     }

    public void OnclickExit()
    {
        Application.Quit();
        playUi.Play();
    }


    public void OnClickLevelSelect()
    {
        mainmenu.SetActive(false);
        levelSelect.SetActive(true);
        playUi.Play();
    }

    public void OnClickOptions()
    {
        mainmenu.SetActive(false);
        options.SetActive(true);
        playUi.Play();
    }

    public void OnClickReturnOptions()
    {
        options.SetActive(false);
        mainmenu.SetActive(true);
        playUi.Play();
    }

    public void OnClickReturnLevelSelect()
    {
        levelSelect.SetActive(false);
        mainmenu.SetActive(true);
        playUi.Play();
    }
}
