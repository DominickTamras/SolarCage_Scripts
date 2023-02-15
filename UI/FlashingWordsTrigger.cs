using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashingWordsTrigger : MonoBehaviour
{
    public FlashingWordsInstance flashingWordsScriptable;
    public MenuManager menuManager;
    public Animator playMinorGlitch;
    public AudioSource instruction;

    bool activateFlash;


    private void Update()
    {
        if(activateFlash == true)
        {
            //FlashingWordsStart();
            menuManager.FlashingWordsManager(flashingWordsScriptable);
            StartCoroutine(CleanseIt());

        }

    }


    public void FlashingWordsStart()
    {
        //tells the menu manager to open the terminal menu with the associated log
        if (flashingWordsScriptable != null)
        {
            menuManager.FlashingWordsManager(flashingWordsScriptable);
        }
        else
        {
            Debug.LogError("This terminal is missing a log scriptable object!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            activateFlash = true;
            FlashingWordsStart();
            gameObject.GetComponent<Collider>().enabled = false;
            instruction.Play();
            playMinorGlitch.Play("MinorGlitch", -1 , 0f);



        }
    }

    IEnumerator CleanseIt()
    {
        yield return new WaitForSeconds(2.5f);
        menuManager.flashingWordText.text = "";
        gameObject.SetActive(false);
        menuManager.wordsCount = 0;
    }

}