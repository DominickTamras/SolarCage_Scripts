using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitOrRestart : MonoBehaviour
{

    [SerializeField] SaveSystem savedInLevel;
    public void OnClickRestart()
    {
        savedInLevel.LevelSavedData();
      
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

   public void OnClickEnd()
    {

        savedInLevel.LevelSavedData();
        SceneManager.LoadScene("MainMenu");
    }

    public void OnClickNextScene()
    {

        savedInLevel.LevelSavedData();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
