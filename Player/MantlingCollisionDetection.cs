using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MantlingCollisionDetection : MonoBehaviour
{
    public bool isColliding;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Climbable"))
        {
            isColliding = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Climbable"))
        {
            isColliding = false;
        }
    }
}
