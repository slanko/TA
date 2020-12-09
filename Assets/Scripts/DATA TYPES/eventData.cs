using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum playerVariables
{
    HEALTH,
    HEALTHPERCENTAGE,
    TRUCKHEALTH,
    CREDIT,
    LUCK,
    JUNK
}



[System.Serializable]
public struct eventStoryBeat
{
    [TextArea(20, 5)] public string beatText;
    public Sprite characterSprite;
    public buttonData[] possibleActions;
}

[System.Serializable]
public struct buttonData
{
    public string buttonName;
    public playerVariables varToChange;
    public float changeAmount;
    public bool endEvent;
    public int nextBeat;
}

[CreateAssetMenu(fileName = "Event Data", menuName = "Trucker's Atlas/Event Data")]
public class eventData : ScriptableObject
{
    public int priority;
    public List<eventStoryBeat> eventBeatList;
    public string eventTitle;
}