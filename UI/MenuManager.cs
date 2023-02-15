using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [Header("Pause")]
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public MeleeAttack meleePauseDisable;
    public GameObject crosshairDisable_PAUSE;
    [HideInInspector]
    public bool endState = false;
    public GameObject levelDataSaved;

    [Header("Terminal")]
    public GameObject terminalUI;
    public GameObject interactUI;
    public TextMeshProUGUI logTitle;
    public TextMeshProUGUI logBody;


    [Header("Flashing Words")]
    public GameObject flashingWordsObject;
    public TextMeshProUGUI flashingWordText;
    public int showWordsAmount = 3;
    public float showWordsTime = 1f;
    public float closeWordsTime = 0.5f;

    [Header("Log Chat")]
    public GameObject chatUI;
    public TextMeshProUGUI chatBody;
    
    [Header("Death menu")]
    public GameObject deathMenu;

    Scene sceneMANAGER;


    /*    [Header("Talking Log")]
        public GameObject talkinglog;*/



    [HideInInspector]
    public int wordsCount = 0;
    bool addCount = false;

 
     float timerShow = 0f;
     float timerClose = 0f;
     bool shown = false;
     bool closed = false;
     bool playerDead = false;

    private TypeWriterEffect checkEnd;


    void Start()
    {
        //checkEnd = FindObjectOfType<TypeWriterEffect>();
        sceneMANAGER = SceneManager.GetActiveScene();

        //Disables cursor
        pauseMenuUI.SetActive(false);
        deathMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;

        //show flashing words
        timerShow = showWordsTime;
        //talkinglog.SetActive(false);

        Resume();
    }


    void Update()
    {
        /*if(checkEnd.isEnded == false)
        {
            FlashingWordsManager();
        }*/
        //FlashingWordsManager();

        //Pause
        if (Input.GetKeyDown(KeyCode.Escape) && endState == false)
        { if (playerDead != true)
            {
                if (GameIsPaused)
                {
                    Resume();
                }
                else
                {
                    OpenPauseMenu();
                    Pause();
                }

            }
        }
    }

    public void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        meleePauseDisable.enabled = false;
        GameIsPaused = true;
        Time.timeScale = 0f;
        crosshairDisable_PAUSE.SetActive(false);

    }

    public void EndPause()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        meleePauseDisable.enabled = true;
        GameIsPaused = false;
        Time.timeScale = 1f;
        crosshairDisable_PAUSE.SetActive(true);
      
    }

    void OpenPauseMenu()
    {
        pauseMenuUI.SetActive(true);
        interactUI.SetActive(false);
        
    }

    public void DeathScreen()
    {
        StartCoroutine(StartDeathScreen());
      /*  Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        deathMenu.SetActive(true);*/
    }

    IEnumerator StartDeathScreen()
    {
        yield return new WaitForSeconds(0.6f);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        deathMenu.SetActive(true);
        playerDead = true;
 
    }

    public void Resume()
    {
        FindObjectOfType<AudioManager>().PlaySound("U.IPress");
        pauseMenuUI.SetActive(false);
        terminalUI.SetActive(false);
        EndPause();
        
    }

    public void Restart()
    {
        FindObjectOfType<AudioManager>().PlaySound("U.IPress");
        EndPause();
        EnemyDeath.enemyCount = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
       
    }

  
    public void Quit()
    {
        Application.Quit();
        
        
        levelDataSaved.GetComponent<SaveSystem>().LevelSavedData();
        FindObjectOfType<AudioManager>().PlaySound("U.IPress");
    }

    public void MainMenuReturn()
    {
        Resume();
        FindObjectOfType<AudioManager>().PlaySound("U.IPress");
        levelDataSaved.GetComponent<SaveSystem>().LevelSavedData();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void OpenTerminal(TerminalLog log)
    {
        //Game paused
        terminalUI.SetActive(true);
       
        Pause();
        FindObjectOfType<AudioManager>().PlaySound("U.IPress");

        //Open termal menu
        logTitle.text = log.title;
        logBody.text = log.log;
    }

    public void CloseTerminal()
    {
        
        terminalUI.SetActive(false);

        Resume();
        FindObjectOfType<AudioManager>().PlaySound("U.IPress");

       
    }

    public void StartChat(ChatLog chatLog)
    {
        chatUI.SetActive(true);
        meleePauseDisable.enabled = false;
        //Pause();
        //chatBody.text = chatLog.chatLog;
    }

    public void EndChat()
    {
        chatUI.SetActive(false);
        meleePauseDisable.enabled = true;


        if (sceneMANAGER.name == "Level_1" || sceneMANAGER.name == "Level_2" || sceneMANAGER.name == "Level_3")
        {
            FindObjectOfType<AudioManager>().PlaySound("BG_MUSIC_LEVEL1");
        }

        if (sceneMANAGER.name == "Level_4" || sceneMANAGER.name == "Level_5" || sceneMANAGER.name == "Level_6")
        {
            FindObjectOfType<AudioManager>().PlaySound("BG_MUSIC_CHAPTER2");
        }

        if (sceneMANAGER.name == "Level_7" || sceneMANAGER.name == "Level_8" || sceneMANAGER.name == "Level_9")
        {
            FindObjectOfType<AudioManager>().PlaySound("BG_MUSIC_CHAPTER3");
        }

    }

    public void ShowWords()
    {
        addCount = false;
        flashingWordsObject.SetActive(true);

    }

    void CloseWords()
    {
        if (!addCount)
        {
            wordsCount++;
            addCount = true;
        }
        flashingWordsObject.SetActive(false);
    }



    public void FlashingWordsManager(FlashingWordsInstance flashWords)
    {
        flashingWordText.text = flashWords.flashWords;

        if (wordsCount < showWordsAmount)
        {
            if (timerShow > 0)
            {
                ShowWords();
                timerShow -= Time.deltaTime;
                closed = false;
            }
            else
            {   
                if (!closed)
                {
                    timerClose = closeWordsTime;
                    closed = true;
                  
                }

            }

            if (timerClose > 0)
            {
                CloseWords();
                timerClose -= Time.deltaTime;
                shown = false;

            }
            else
            {
                if (!shown)
                { 
                    timerShow = showWordsTime;
                    shown = true;

                  
                }
            }
            
        }

        

    }
}
