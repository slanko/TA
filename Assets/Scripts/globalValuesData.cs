using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Global Value Data", menuName = "Trucker's Atlas/Global Value Data")]
public class globalValuesData : ScriptableObject
{
    public enum factionType
    {
        BANDIT,
        FREETRADE,
        CORPORATION,
        FACTIONLESS
    }

    public enum itemType
    {
        STUFF,
        JUNK,
        TRASH
    }
    [System.Serializable]
    public struct valueDataEntry
    {
        public itemType item;
        public int itemValue;
    }

    public float globalRepMult;
    
    public valueDataEntry[] globalValues;
}
