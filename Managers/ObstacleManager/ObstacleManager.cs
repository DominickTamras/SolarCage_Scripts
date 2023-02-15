using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObstacleManager : MonoBehaviour
{
    public static ObstacleManager current;
    private void Awake()
    {
        current = this;
    }

    // Update is called once per frame
    public event Action<int> onObstacleTriggerEnter;
    public void ObstacleTriggerEnter(int id)
    {
        if(onObstacleTriggerEnter != null)
        {
            onObstacleTriggerEnter(id);
        }
    }

    public event Action<int> onObstacleTriggerExit;
    public void ObstacleTriggerExit(int id)
    {
        if(onObstacleTriggerExit != null)
        {
            onObstacleTriggerExit(id);
        }
    }

  
}
