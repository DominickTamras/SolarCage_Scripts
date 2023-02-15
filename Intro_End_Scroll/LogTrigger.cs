using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogTrigger: MonoBehaviour
{
    public ChatLog chatLog;
    public LevelDataSO enteredLevel;
    public MenuManager manager;


    public void StartChat()
    {
        if (chatLog != null)
        {
            manager.StartChat(chatLog);
        }

        else
        {
            Debug.LogError("This terminal is missing a log scriptable object!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            enteredLevel.isCompleted = true;
            StartChat();
            FindObjectOfType<AudioManager>().PlaySound("StartUp");
            gameObject.GetComponent<Collider>().enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
