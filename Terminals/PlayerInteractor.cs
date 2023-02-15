using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] float interactRange;
    public GameObject interactScreen;
    private bool pressedOpen;
    void Update()
    {
        if (!MenuManager.GameIsPaused)
        {
            InteractFinder();
        }
    }

    void InteractFinder()
    {
        //Raycast
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, interactRange))
        {
            //If a terminal is found
            TerminalObject termObj = hit.transform.GetComponent<TerminalObject>();
            if (termObj != null)
            {
                //Show interact key popup
                interactScreen.SetActive(true);
                //Allow interact with terminal
                //If interact, use the menumanager to open terminal menu
                if (Input.GetKeyDown(KeyCode.E))
                {
                
                        termObj.OpenTerminal();
                    if(interactScreen != null)
                    {
                        interactScreen.SetActive(false);
                    }
                  
                    

                }
            }

            else
            {
                if(interactScreen != null)
                {
                    interactScreen.SetActive(false);
                }
           
            }

            IEnumerator closeTerminalTrue()
            {
                yield return new WaitForSeconds(0.1f);
                pressedOpen = true;
            }
        }
    }
}
