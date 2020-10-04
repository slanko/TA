using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using static UnityEngine.UI.Button;

[System.Serializable]
public struct eventStruct
{
    public string entryName;
    public eventData eventMaster;
}
[System.Serializable]
public struct effectStruct
{
    public string effectName;
    public UnityEvent effect;
}

public class randomEventScript : MonoBehaviour
{
    [SerializeField] Text eventHeader, eventText;
    [SerializeField] Image eventCharacter;
    [SerializeField] GameObject eventPopup;
    [SerializeField] GameObject randomEventCanvas;
    [SerializeField] playerResources pR;
    [SerializeField] Slider timeScaleSlider;
    public List<eventStruct> eventList;
    public eventData currentEvent;
    int eventCounter = 0;
    public List<Transform> currentButtons;

    [SerializeField, Header("Timer Stuff")] int randomEventTimeMin;
    [SerializeField] int randomEventTimeMax;

    [SerializeField] GameObject buttonPrefab, buttonParent;

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
        var ev = eventList [Random.Range(0, eventList.Count)];
        currentEvent = ev.eventMaster;
        eventHeader.text = currentEvent.eventTitle;
        eventCharacter.sprite = currentEvent.eventBeatList[eventCounter].characterSprite;
        eventCounter = 0;
        eventText.text = currentEvent.eventBeatList[eventCounter].beatText;
        StartCoroutine(randomEvent(Random.Range(randomEventTimeMin, randomEventTimeMax)));
        populateButtons(0);
    }



    void populateButtons(int beatNum)
    {
        clearButtons();
        foreach (var effect in currentEvent.eventBeatList[beatNum].possibleActions)
        {
            // instantiate a button
            var button = Instantiate(buttonPrefab, buttonParent.transform).GetComponent<Button>();
            button.GetComponentInChildren<Text>().text = effect.buttonName;
            currentButtons.Add(button.transform);
            if (effect.endEvent == true)
            {
                button.onClick.AddListener(delegate { endEvent(); });
            }
            else
            {
                button.onClick.AddListener(delegate { changeBeat(effect.nextBeat); });
            }
        }
    }

    void clearButtons()
    {
        foreach (Transform gazoom in currentButtons)
        {
            Destroy(gazoom.gameObject);
        }
        currentButtons.Clear();
    }


    //button functions

    public void changeHealth(float changeAmount)
    {

    }

    public void endEvent()
    {
        randomEventCanvas.SetActive(false);
        Time.timeScale = timeScaleSlider.value;
    }

    public void changeBeat(int beatNum)
    {
        eventText.text = currentEvent.eventBeatList[beatNum].beatText;
        populateButtons(beatNum);
    }
    
}
