using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalObject : MonoBehaviour
{
    public TerminalLog log;
    public MenuManager menuManager;
   

    public void OpenTerminal()
    {
        //tells the menu manager to open the terminal menu with the associated log
        if(log != null)
        {
            menuManager.OpenTerminal(log);
        }
        else
        {
            Debug.LogError("This terminal is missing a log scriptable object!");
        }
    }

    public void CloseTerminal()
    {
        if (log != null)
        {
            menuManager.CloseTerminal();
        }
    }
}
