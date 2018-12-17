using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreationCountdown : MonoBehaviour
{
    public Image fillBar;
    public Text timeRemainingText;
    public int totalHours;
    public int totalMinutes;
    public int totalSeconds;
    public bool purchased = false;
    public float counter = 0;
    DateTime thisTime;
    private float totalTimeForCreation;
    public string objectToSpawn;

    // Use this for initialization
    void Start()
    {
       thisTime = new DateTime(2018, 1, 1, totalHours, totalMinutes, totalSeconds);
    }

    // Update is called once per frame
    void Update()
    {
        if (purchased)
        {
            CompleteNewPurchase();
        }

    }

    public void CompleteNewPurchase()
    {
        counter += Time.deltaTime;
        thisTime = new DateTime(2018, 1, 1, totalHours, totalMinutes, totalSeconds);
        thisTime = thisTime.AddSeconds(-counter);

        if (counter / totalTimeForCreation <= 1)
        {
            fillBar.fillAmount = counter / totalTimeForCreation;
            timeRemainingText.text = thisTime.ToString("HH:mm:ss");
        }
        else
        {
            purchased = false;
            fillBar.fillAmount = 0;
            counter = 0;
            thisTime = new DateTime(2018, 1, 1, 00, 00, 00);
            timeRemainingText.text = thisTime.ToString("HH:mm:ss");
            GetComponent<SpawnObject>().SpawnGivenObject(objectToSpawn);
        }
    }

    public bool GetPurchased()
    {
        return purchased;
    }

    public void SetPurchased(bool purchased)
    {
        totalTimeForCreation = (totalHours * 3600) + (totalMinutes * 60) + totalSeconds;
        thisTime = new DateTime(2018, 1, 1, totalHours, totalMinutes, totalSeconds);  
        this.purchased = purchased;
    }
}
