using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectShatter : MonoBehaviour
{
    public GameObject modelSwap; 

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "MeleeAttack")
        {
            Instantiate(modelSwap, transform.position, transform.rotation);
            Destroy(gameObject);
        }    
    }
}
