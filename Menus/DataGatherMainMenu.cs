using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DataGatherMainMenu : MonoBehaviour
{
    public LevelDataSO check;

    public Button button;

    public TextMeshProUGUI kills;

    public TextMeshProUGUI timeMin;

    public TextMeshProUGUI timeSec;

    public TextMeshProUGUI deaths;



    public void Update()
    {
        if(check.isCompleted == true)
        {
            button.interactable = true;
        }

        else
        {
            button.interactable = false;
        }

        kills.text = check.enemiesKilled.ToString();

        timeMin.text = check.minutes.ToString();

        timeSec.text = check.seconds.ToString();

        deaths.text = check.deaths.ToString();

    }

}
