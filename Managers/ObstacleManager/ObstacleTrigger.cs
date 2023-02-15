using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleTrigger : MonoBehaviour
{
    public int id;
     private void OnTriggerEnter(Collider other)
    {
        ObstacleManager.current.ObstacleTriggerEnter(id);

    }

     private void OnTriggerExit(Collider other)
    {
        ObstacleManager.current.ObstacleTriggerExit(id);
    }

}
