﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public struct eventStoryBeat
{
    [Multiline(4)] public string beatText;
    public Sprite characterSprite;
}

[CreateAssetMenu(fileName = "Event Data", menuName = "Trucker's Atlas/Event Data")]
public class eventData : ScriptableObject
{
    public List<eventStoryBeat> eventBeatList;
    public string eventTitle;
}