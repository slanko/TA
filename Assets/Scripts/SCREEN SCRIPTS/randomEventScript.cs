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

public class randomEventScript : MonoBehaviour
{
    [SerializeField] Text eventHeader, eventText;
    [SerializeField] Image eventCharacter;
    [SerializeField] GameObject eventPopup;
    [SerializeField] GameObject randomEventCanvas;
    [SerializeField] playerResources pR;
    [SerializeField] playerScript pS;
    [SerializeField] Slider timeScaleSlider;
    [SerializeField] eventStruct testEvent;
    wordReplacements wR;
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
        while (counter > 0)
        {
            yield return new WaitForSeconds(1);
            counter--;
        }
        randomEventFunction();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            randomEventFunction();
        }
    }

    private void Start()
    {
        StartCoroutine(randomEvent(Random.Range(randomEventTimeMin, randomEventTimeMax)));
        wR = GetComponent<wordReplacements>();
    }

    void randomEventFunction()
    {
        if (pS.currentState == playerScript.playerState.TRAVELLING)
        {
            eventPopup.SetActive(true);
            Time.timeScale = 0;
            currentEvent = pickRandomEvent().eventMaster;
            Debug.Log("random event happens: " + currentEvent);
            eventHeader.text = currentEvent.eventTitle;
            eventCharacter.sprite = currentEvent.eventBeatList[eventCounter].characterSprite;
            eventCounter = 0;
            eventText.text = currentEvent.eventBeatList[eventCounter].beatText;
            //replace words section
            eventText.text = eventText.text.Replace("<<INSULTNAME>>", wR.insultNames[Random.Range(0, wR.insultNames.Length)]);
            StartCoroutine(randomEvent(Random.Range(randomEventTimeMin, randomEventTimeMax)));
            populateButtons(0);
        }
        else
        {
            Debug.Log("random event failed due to player state: " + pS.currentState);
        }
    }
    public eventStruct pickRandomEvent()
    {
        eventStruct currentEvent;
        currentEvent = eventList[Random.Range(0, eventList.Count)];
        eventStruct eventPicked = new eventStruct();
        for (int i = 0; i < currentEvent.eventMaster.priority; i++)
        {
            eventPicked = eventList[Random.Range(0, eventList.Count)];
            if ((eventPicked.eventMaster.priority - currentEvent.eventMaster.priority) <= 0)
            {
                break;
            }
        }
        return eventPicked;
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

            if (effect.changeAmount != 0)
            {
                switch (effect.varToChange)
                {
                    case playerVariables.HEALTH:
                        button.onClick.AddListener(delegate { changeHealth(effect.changeAmount); });
                        break;

                    case playerVariables.HEALTHPERCENTAGE:
                        button.onClick.AddListener(delegate { changeHealthPercent(effect.changeAmount); });
                        break;

                    case playerVariables.CREDIT:
                        button.onClick.AddListener(delegate { changeCredits(effect.changeAmount); });
                        break;

                    case playerVariables.LUCK:
                        button.onClick.AddListener(delegate { changeLuck(effect.changeAmount); });
                        break;

                    case playerVariables.TRUCKHEALTH:
                        button.onClick.AddListener(delegate { changeTruckHealth(effect.changeAmount); });
                        break;

                    case playerVariables.JUNK:
                        button.onClick.AddListener(delegate { changeJunkAmount(effect.changeAmount); });
                        break;

                }
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
        pR.playerHealth = pR.playerHealth + changeAmount;
    }

    public void changeHealthPercent(float changeAmount)
    {
        pR.playerHealth = pR.playerHealth * changeAmount;
    }

    public void changeCredits(float changeAmount)
    {
        pR.playerCredit = pR.playerCredit + changeAmount;
    }

    public void changeLuck(float changeAmount)
    {
        pR.playerLuck = pR.playerLuck + changeAmount;
    }

    public void changeTruckHealth(float changeAmount)
    {
        pR.truckHealth = pR.truckHealth + changeAmount;
    }

    //item values
    public void changeJunkAmount(float changeAmount)
    {
        pR.giveItem(globalValuesData.itemType.JUNK, changeAmount);
    }
    //we can do this BETTER

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
    //and we can do THIS BETTER

    public void addEventFromSublist(eventStruct eventToAdd)
    {
        bool doubleUpEvent = false;
        foreach(eventStruct ev in eventList)
        {
            if(eventToAdd.entryName == ev.entryName)
            {
                doubleUpEvent = true;
                Debug.Log("found double up of " + ev.entryName);
                break;
            }
        }
        if(doubleUpEvent == false)
        {
            eventList.Add(eventToAdd);
        }
    }
    public void removeEventFromEventlist(eventStruct eventToRemove)
    {
        eventList.Remove(eventToRemove);
    }

}
