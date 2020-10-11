﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerResources : MonoBehaviour
{
    public float playerHealth, truckHealth, playerCredit, playerLuck;

    //FACTION REP
    public float banditRep, freeTradeRep, corporationRep, globalRep;


    [SerializeField] Slider healthBar;
    bool playerIsAlive = true;
    [SerializeField] Animator deathScreen;
    [SerializeField] Text creditsText;

    [System.Serializable]
    public struct InventoryEntry
    {
        public globalValuesData.itemType entryType;
        public float amountHeld;
    }

    public List<InventoryEntry> playerInventory;

    // Update is called once per frame
    void Update()
    {
        healthBar.value = playerHealth;

        if(playerHealth <= 0 && playerIsAlive)
        {
            //game OVERRRRRRRRRRRRR
            Debug.Log("you fricking died");
            deathScreen.SetTrigger("die");
            playerIsAlive = false;
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            playerHealth = 0;
        }

        creditsText.text = "CREDITS: $" + playerCredit.ToString();

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            giveItem(globalValuesData.itemType.STUFF, 1);
        }       
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            giveItem(globalValuesData.itemType.JUNK, 1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            giveItem(globalValuesData.itemType.TRASH, 1);
        }
    }

    public void giveItem(globalValuesData.itemType type, float amount)
    {
        bool foundItemType = false;
        for(int i = 0; i < playerInventory.Count; i++)
        {
            if(playerInventory[i].entryType == type)
            {
                foundItemType = true;
                playerInventory[i] = new InventoryEntry { entryType = type, amountHeld = playerInventory[i].amountHeld + amount };
            }
        }
        if(foundItemType == false)
        {
            playerInventory.Add(new InventoryEntry { entryType = type, amountHeld = amount });
        }
    }

    public void reputationChange(float changeAmount, globalValuesData.factionType faction)
    {
        switch (faction)
        {
            case globalValuesData.factionType.BANDIT:
                banditRep = banditRep + changeAmount;
                freeTradeRep = freeTradeRep - (changeAmount * 0.3f);
                corporationRep = corporationRep - (changeAmount * 0.35f);
                break;

            case globalValuesData.factionType.FREETRADE:
                freeTradeRep = freeTradeRep + changeAmount;
                banditRep = banditRep - (changeAmount * 0.2f);
                corporationRep = corporationRep - (changeAmount * 0.5f);

                break;

            case globalValuesData.factionType.CORPORATION:
                corporationRep = corporationRep + changeAmount;
                banditRep = banditRep - (changeAmount * 0.5f);
                freeTradeRep = freeTradeRep - (changeAmount * 0.5f);

                break;

            case globalValuesData.factionType.FACTIONLESS:
                globalRep = globalRep + changeAmount;
                corporationRep = corporationRep + (changeAmount * 0.1f);
                freeTradeRep = freeTradeRep + (changeAmount * 0.3f);
                banditRep = banditRep + (changeAmount * 0.5f);

                break;
        }

    }
}