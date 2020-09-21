using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[System.Serializable]
public struct eventStoryBeat
{
    [Multiline(4)] public string beatText;
    public Sprite characterSprite;
    public UnityEvent eventEffects;
}

[CreateAssetMenu(fileName = "Event Data", menuName = "Trucker's Atlas/Event Data")]
public class eventData : ScriptableObject
{
    public List<eventStoryBeat> eventBeatList;
    public string eventTitle;
}