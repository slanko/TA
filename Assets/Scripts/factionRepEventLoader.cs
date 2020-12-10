using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class factionRepEventLoader : MonoBehaviour
{
    //gonna say first and foremost i don't want this script running every hour, only when it needs to. check if values change from previous hour if you can.
    float previousBanditRep = 0, previousCorpRep = 0, previousFreeTradeRep = 0, previousGlobalRep = 0;

    [SerializeField] sublistData[] banditSublists, corporationSublists, freeTradeSublists, globalSublists;
    playerResources pR;
    randomEventScript rES;

    private void Awake()
    {
        pR = GetComponent<playerResources>();
        rES = GetComponent<randomEventScript>();
    }

    public void loadFactionEvents()
    {
        factionRepCheck(globalValuesData.factionType.BANDIT);
        factionRepCheck(globalValuesData.factionType.CORPORATION);
        factionRepCheck(globalValuesData.factionType.FREETRADE);
        factionRepCheck(globalValuesData.factionType.FACTIONLESS);
        previousBanditRep = pR.banditRep;
        previousCorpRep = pR.corporationRep;
        previousFreeTradeRep = pR.freeTradeRep;
        previousGlobalRep = pR.globalRep;
    }

    public void factionRepCheck(globalValuesData.factionType faction)
    {
        float factionRep = 0, previousFactionRep = 0;
        sublistData[] factionSublist = new sublistData[0];

        switch (faction)
        {
            case globalValuesData.factionType.BANDIT:
                factionRep = pR.banditRep;
                previousFactionRep = previousBanditRep;
                factionSublist = banditSublists;
                break;

            case globalValuesData.factionType.FREETRADE:
                factionRep = pR.freeTradeRep;
                previousFactionRep = previousFreeTradeRep;
                factionSublist = freeTradeSublists;
                break;

            case globalValuesData.factionType.CORPORATION:
                factionRep = pR.corporationRep;
                previousFactionRep = previousCorpRep;
                factionSublist = corporationSublists;
                break;

            case globalValuesData.factionType.FACTIONLESS:
                factionRep = pR.globalRep;
                previousFactionRep = previousGlobalRep;
                factionSublist = globalSublists;
                break;
        }
        //the good side of things
        if (factionRep >= 100 && previousFactionRep < 100)
        {
            foreach(eventStruct ev in factionSublist[0].sublist)
            {
                rES.addEventFromSublist(ev);
            }
            Debug.Log("Added " + faction + "sublist set 1");
        }
        if(factionRep >= 250 && previousFactionRep < 250)
        {
            foreach(eventStruct ev in factionSublist[1].sublist)
            {
                rES.addEventFromSublist(ev);
            }
            Debug.Log("Added " + faction + "sublist set 2");
        }
        if(factionRep >= 400 && previousFactionRep < 400)
        {
            foreach(eventStruct ev in factionSublist[2].sublist)
            {
                rES.addEventFromSublist(ev);
            }
            Debug.Log("Added " + faction + "sublist set 3");
        }
        //the other side of the good side of things
        if(previousFactionRep >= 400 && factionRep < 400)
        {
            foreach(eventStruct ev in factionSublist[2].sublist)
            {
                rES.removeEventFromEventlist(ev);
            }
            Debug.Log("Removed " + faction + "sublist set 3");
        }
        if (previousFactionRep >= 250 && factionRep < 250)
        {
            foreach (eventStruct ev in factionSublist[1].sublist)
            {
                rES.removeEventFromEventlist(ev);
            }
            Debug.Log("Removed " + faction + "sublist set 2");
        }
        if (previousFactionRep >= 100 && factionRep < 100)
        {
            foreach (eventStruct ev in factionSublist[0].sublist)
            {
                rES.removeEventFromEventlist(ev);
            }
            Debug.Log("Removed " + faction + "sublist set 1");
        }

        //the BAD side of things
        if(factionRep <= -100 && previousFactionRep > -100)
        {
            foreach (eventStruct ev in factionSublist[3].sublist)
            {
                rES.addEventFromSublist(ev);
            }
            Debug.Log("Added " + faction + "sublist set 4");
        }
        if(factionRep <= -250 && previousFactionRep > -250)
        {
            foreach (eventStruct ev in factionSublist[4].sublist)
            {
                rES.addEventFromSublist(ev);
            }
            Debug.Log("Added " + faction + "sublist set 5");
        }
        if(factionRep <= -400 && previousFactionRep > -400)
        {
            foreach (eventStruct ev in factionSublist[5].sublist)
            {
                rES.addEventFromSublist(ev);
            }
            Debug.Log("Added " + faction + "sublist set 6. YOU FUCKED UP");
        }
        //the other side of the bad side of things
        if (factionRep > -400 && previousFactionRep <= -400)
        {
            foreach (eventStruct ev in factionSublist[5].sublist)
            {
                rES.removeEventFromEventlist(ev);
            }
            Debug.Log("Removed " + faction + "sublist set 6");
        }
        if (factionRep > -250 && previousFactionRep <= -250)
        {
            foreach (eventStruct ev in factionSublist[4].sublist)
            {
                rES.removeEventFromEventlist(ev);
            }
            Debug.Log("Removed " + faction + "sublist set 5");
        }
        if (factionRep > -100 && previousFactionRep <= -100)
        {
            foreach (eventStruct ev in factionSublist[3].sublist)
            {
                rES.removeEventFromEventlist(ev);
            }
            Debug.Log("Removed " + faction + "sublist set 4");
        }
    }
}
