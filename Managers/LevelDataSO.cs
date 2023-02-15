using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
[CreateAssetMenu(menuName = "Scriptable Objects/Level_Data")]
public class LevelDataSO : ScriptableObject
{
    public bool isCompleted;

    public string iD;
    //Time Score
    public float minutes;
    public float seconds;

    //Enemy Score
    public int totalEnemies;
    public int enemiesKilled;

    //Level Restarted
    public int deaths;

 
}
