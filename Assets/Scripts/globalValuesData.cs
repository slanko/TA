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
        TRASH,
        JUNK,
        LUXURIES,
        CORPCREDIT,
        FOOD,
        MEDSUPS,
        DIESEL,
        TOOLS,
        CORPUNIFORM,
        BANDUNIFORM,
        DRUG,
        WEAPONS
    }
    [System.Serializable]
    public struct valueDataEntry
    {
        public itemType item;
        public int itemValue;
        [TextArea]
        public string description;
    }

    public float globalRepMult;
    
    public valueDataEntry[] globalValues;
}
