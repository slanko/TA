using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum restQuality
{
    BAD,
    OKAY,
    GOOD
}

[CreateAssetMenu(fileName = "Rest Stop Data", menuName = "Trucker's Atlas/Rest Stop Data")]
public class restStopData : ScriptableObject
{
    public string stopName;
    [Tooltip("Affects the rate of health gain from resting here.\nBAD = 0.5x\nOKAY = 1x\nGOOD = 1.25x")]
    public restQuality quality;
    [TextArea (20, 5), Tooltip("Description of the rest area.")] 
    public string stopDesc;
    [Tooltip("The yellow title bar for the second screen.\n(eg. \"They let you sleep on the floor\")")]
    public string restDesc;
    [Tooltip("Sprite of the player character resting.")]
    public Sprite restImage;

}
