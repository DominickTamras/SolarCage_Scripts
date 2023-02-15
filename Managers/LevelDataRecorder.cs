using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

[Serializable]
public class LevelDataRecorder : MonoBehaviour
{
    //CREATE A SCRIPTABLE OBJECT FOR EACH LEVEL
    //CREATE A LevelDataRecorder FOR EACH LEVEL
    //PLACE EndTrigger SCRIPT ONTO THE END COLLIDER AND DRAG IN REFERENCE TO LevelDataRecorder

    public LevelDataSO levelData;

    //Timer
    private bool runCounter = true;
    private float timer = 0f;
    private float minutes;
    private float seconds;

    public float currMinutes;
    public float currSeconds;
    public int currDeaths;
    public bool currComplete;
    //Enemy Score
    public int enemiesKilled = 0;
    public int totalEnemies = 0;

    private void Start()
    {
        totalEnemies = EnemyDeath.enemyCount;
        currDeaths = levelData.deaths;
        totalEnemies = levelData.totalEnemies;
        currComplete = levelData.isCompleted;

    }

    void Update()
    {
        timerCounter();
        //levelData.deaths = PlayerGoByeBye.deathsSTATIC;
        levelData.deaths = currDeaths;

    }

    //Still need to add functionality for level 1 where at the very start, the timer doesn't start until cutscene is over.
    private void timerCounter()
    {
        if(runCounter == true)
        {
            timer = Time.time;
            minutes = ((int)timer / 60);
            seconds = (timer % 60);
           // Mathf.RoundToInt(seconds * 1000); Figure out how to round and get rid of last 3 numbers in calc
        }
    }

    public void dataRecorder()
    {
        //Local data
        currMinutes = minutes;
        currSeconds = seconds;
        enemiesKilled = totalEnemies - EnemyDeath.enemyCount; 

        runCounter = false;

        //High score data
        if(currMinutes <= levelData.minutes || levelData.minutes == 0)
        {
            if(currSeconds <= levelData.seconds || levelData.seconds == 0)
            {
                levelData.minutes = currMinutes;
                levelData.seconds = currSeconds;
                levelData.enemiesKilled = enemiesKilled;
            }
        }
        levelData.isCompleted = true;
        //currComplete = true;
    }
}
