using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public LevelDataSO lv1, lv2, lv3, lv4, lv5, lv6, lv7, lv8, lv9;

    public void loadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
