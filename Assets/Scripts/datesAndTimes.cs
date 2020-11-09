using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class datesAndTimes : MonoBehaviour
{
    GameObject GOD;
    godPointToThing gPTT;
    godPointToThing.timeStruct time;
    int daysInTheMonth;
    void Start()
    {
        GOD = GameObject.Find("GOD");
        gPTT = GOD.GetComponent<godPointToThing>();
        time = gPTT.theTime;
    }

    void Update()
    {
        time = gPTT.theTime;
        if (time.hour > 24)
        {
            time.hour = 0;
            time.day++;
        }
        switch (time.month)
        {
            case 1:
            case 3:
            case 5:
            case 7:
            case 8:
            case 10:
            case 12:
                daysInTheMonth = 31;
                break;

            case 2:
            case 4:
            case 6:
            case 9:
            case 11:
                daysInTheMonth = 30;
                break;
        }

        if(time.day > daysInTheMonth)
        {
            time.day = 1;
            time.month++;
        }
        if(time.month > 12)
        {
            time.month = 1;
            time.year++;
        }

        gPTT.theTime = time;
    }

    public string getDate()
    {
        string theDate = time.day.ToString("D2") + "/" + time.month.ToString("D2") + "/" + time.year.ToString("D4");
        return theDate;
    }

    public string getTime()
    {
        string theTime = time.hour.ToString("D2") + ":" + time.minute.ToString("D2");
        return theTime;
    }

    public string getAdjustedTime(int minute, int hour)
    {
        string adjustedTime = (time.hour + hour).ToString("D2") + ":" + (time.minute + minute).ToString("D2");
        return adjustedTime;
    }

    public string getAdjustedDate(int day, int month, int year)
    {
        string adjustedDate = (time.day + day).ToString("D2") + "/" + (time.month + month).ToString("D2") + "/" + (time.year + year).ToString("D4");
        return adjustedDate;
    }
}
