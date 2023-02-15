using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipText : MonoBehaviour
{
    public GameObject skipText;

    public GameObject player;

    public MenuManager enableArm;

    Scene scene;

    private void Awake()
    {
        scene = SceneManager.GetActiveScene();
    }
    public void  OnClickSkip()
    {
        skipText.SetActive(false);

        FindObjectOfType<AudioManager>().StopSound("StartUp");

        player.SetActive(true);

        enableArm.meleePauseDisable.enabled = true;

        if(scene.name == "Level_1" || scene.name == "Level_2" || scene.name == "Level_3")
        {
            FindObjectOfType<AudioManager>().PlaySound("BG_MUSIC_LEVEL1");
        }

        if (scene.name == "Level_4" || scene.name == "Level_5" || scene.name == "Level_6")
        {
            FindObjectOfType<AudioManager>().PlaySound("BG_MUSIC_CHAPTER2");
        }

        if (scene.name == "Level_7" || scene.name == "Level_8" || scene.name == "Level_9")
        {
            FindObjectOfType<AudioManager>().PlaySound("BG_MUSIC_CHAPTER3");
        }


        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


}
