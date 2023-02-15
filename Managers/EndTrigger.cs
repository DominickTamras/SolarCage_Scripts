using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndTrigger : MonoBehaviour
{
    //PLACE THIS SCRIPT ON END OF LEVEL COLLIDER
    //REFERENCE THE LevelDataRecorder SCRIPT

    public LevelDataRecorder dataRecorder;

    public GameObject scoreScreen;

    public GameObject playerStop;

    public GameObject footStepStop;

    public GameObject crosshairDisable;

    public MenuManager menuManagerAccess;

    public MeleeAttack meleeDisableEnd;

    public Animator playAnim2;

    public TextMeshProUGUI kills;

    public TextMeshProUGUI mins;

    public TextMeshProUGUI deaths;

    public GameObject turnOffCam;

    public TextMeshProUGUI secs;
    
    private bool endReached;

    


    public void Update()
    {
        kills.text = dataRecorder.enemiesKilled.ToString();

        mins.text = dataRecorder.currMinutes.ToString();

        secs.text = dataRecorder.currSeconds.ToString();

        deaths.text = dataRecorder.currDeaths.ToString();

        if(endReached == true)
        {
            FindObjectOfType<AudioManager>().StopSound("Slide");
          
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        PlayerMovement player = other.GetComponent<PlayerMovement>();
        if(player != null)
        {
            StartCoroutine(StartEndScreen());
            FindObjectOfType<AudioManager>().PlaySound("Player_Endlevel1");
            FindObjectOfType<AudioManager>().PlaySound("Player_Endlevel2");
            FindObjectOfType<AudioManager>().StopSound("Slide");
            FindObjectOfType<AudioManager2>().StopSound("Enemy_Shoot");
            turnOffCam.SetActive(false);
            playerStop.SetActive(false);
            footStepStop.SetActive(false);
            playAnim2.Play("GlitchEffect");
            meleeDisableEnd.enabled = false;
            dataRecorder.dataRecorder();
            endReached = true;
            menuManagerAccess.endState = true;
            crosshairDisable.SetActive(false);
            

          
        }
    }

    IEnumerator StartEndScreen()
    {
        yield return new WaitForSeconds(0.6f);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        scoreScreen.SetActive(true);


    }
}
