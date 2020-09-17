using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class randomEventScript : MonoBehaviour
{
    [SerializeField] Text eventHeader, eventText;
    [SerializeField] Image eventCharacter;
    [SerializeField] GameObject eventPopup;
    public List<eventData> eventList;
    public eventData currentEvent;
    int eventCounter = 0;

    [SerializeField, Header("Timer Stuff")] int randomEventTimeMin;
    [SerializeField] int randomEventTimeMax;

    IEnumerator randomEvent(int seconds)
    {
        int counter = seconds;
        while(counter > 0)
        {
            yield return new WaitForSeconds(1);
            counter--;
        }
        randomEventFunction();
    }

    private void Start()
    {
        StartCoroutine(randomEvent(Random.Range(randomEventTimeMin, randomEventTimeMax)));
    }

    void randomEventFunction()
    {
        Debug.Log("random event happens uya");
        eventPopup.SetActive(true);
        Time.timeScale = 0;
        currentEvent = eventList[Random.Range(0, eventList.Count)];
        eventHeader.text = currentEvent.eventTitle;
        eventCharacter.sprite = currentEvent.eventBeatList[eventCounter].characterSprite;
        eventCounter = 0;
        eventText.text = currentEvent.eventBeatList[eventCounter].beatText;
        StartCoroutine(randomEvent(Random.Range(randomEventTimeMin, randomEventTimeMax)));
    }
}
