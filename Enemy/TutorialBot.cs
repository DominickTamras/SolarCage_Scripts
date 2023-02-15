using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class TutorialBot : MonoBehaviour
{
    public GameObject botSpawn;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            botSpawn.SetActive(true);
            botSpawn.transform.GetChild(0).GetComponent<VisualEffect>().Play();
            botSpawn.transform.GetChild(0).GetComponent<AudioSource>().Play();
        }
    }
}
