﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class playerResources : MonoBehaviour
{
    public float playerHealth, truckHealth, playerCredit, playerLuck;
    public const float TruckMaxHealth = 1000;

    //FACTION REP
    public int banditRep, freeTradeRep, corporationRep, globalRep;

    //munchies
    public float playerEatRate;
    public float foodRationValue = 1;
    public bool resting = false;

    //truck speed nav stuff
    NavMeshAgent nav;
    float truckHealthDecreaseRate;

    [SerializeField] Slider healthBar, truckSpeedSlider, truckHealthBar;
    public Slider foodRationSlider;
    bool playerIsAlive = true;
    [SerializeField] Animator deathScreen;
    [SerializeField] Text foodRationText, truckSpeedText;
    GameObject GOD;
    godPointToThing gPTT;
    [System.NonSerialized]
    public restStopData rSD;

    [System.Serializable]
    public struct InventoryEntry
    {
        public globalValuesData.itemType entryType;
        public float amountHeld;
    }

    public List<InventoryEntry> playerInventory;

    private void Start()
    {
        GOD = GameObject.Find("GOD");
        gPTT = GOD.GetComponent<godPointToThing>();
        nav = gPTT.PLAYER.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = playerHealth;
        if (resting == false)
        {
            switch (foodRationSlider.value)
            {
                case 0:
                    foodRationValue = 3;
                    foodRationText.text = "zero servings";
                    break;

                case 1:
                    foodRationValue = 2;
                    foodRationText.text = "one serving";
                    break;

                case 2:
                    foodRationValue = 1;
                    foodRationText.text = "two servings";
                    break;

                case 3:
                    foodRationValue = 0;
                    foodRationText.text = "three servings";
                    break;
            }
        }
        if (resting == false && gPTT.PLAYER.currentState == playerScript.playerState.TRAVELLING)
        {
            if (getItemAmount(globalValuesData.itemType.FOOD) > 0)
            {
                playerHealth = playerHealth - .5f * Time.deltaTime * foodRationValue;
            }
            else
            {
                playerHealth = playerHealth - .5f * Time.deltaTime * 5f;
                foodRationText.text = "no food!!";
            }
        }
        if (resting == true)
        {
            if (getItemAmount(globalValuesData.itemType.FOOD) > 0)
            {
                switch (rSD.quality)
                {
                    case restQuality.BAD:
                        playerHealth = playerHealth + .5f * Time.deltaTime * 2f;
                        break;

                    case restQuality.OKAY:
                        playerHealth = playerHealth + 1f * Time.deltaTime * 2f;
                        break;

                    case restQuality.GOOD:
                        playerHealth = playerHealth + 1.25f * Time.deltaTime * 2f;
                        break;
                }
            }
            else
            {
                switch (rSD.quality)
                {
                    case restQuality.BAD:
                        playerHealth = playerHealth + .5f * Time.deltaTime * -1f;
                        break;

                    case restQuality.OKAY:
                        playerHealth = playerHealth + 1f * Time.deltaTime * .5f;
                        break;

                    case restQuality.GOOD:
                        playerHealth = playerHealth + 1.25f * Time.deltaTime * -.25f;
                        break;
                }
            }

        }

        //truck speed stuff
        switch (truckSpeedSlider.value)
        {
            case 0:
                truckSpeedText.text = "slow";
                nav.speed = 1.5f;
                nav.acceleration = 1;
                truckHealthDecreaseRate = .25f;
                break;
            case 1:
                truckSpeedText.text = "normal";
                nav.speed = 3.5f;
                nav.acceleration = 1;
                truckHealthDecreaseRate = .5f;
                break;

            case 2:
                truckSpeedText.text = "fast";
                nav.speed = 5f;
                nav.acceleration = 5;
                truckHealthDecreaseRate = 1f;
                break;

            case 3:
                truckSpeedText.text = "extra fast";
                nav.speed = 10f;
                nav.acceleration = 10;
                truckHealthDecreaseRate = 2f;
                break;
        }
        if (gPTT.PLAYER.currentState == playerScript.playerState.TRAVELLING)
        {
            truckHealth = truckHealth - truckHealthDecreaseRate * Time.deltaTime;
        }

        truckHealthBar.value = truckHealth;

        if (playerHealth <= 0 && playerIsAlive)
        {
            //game OVERRRRRRRRRRRRR
            Debug.Log("you fricking died");
            deathScreen.SetTrigger("die");
            playerIsAlive = false;
        }
        if (playerHealth > 1000)
        {
            playerHealth = 1000;
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            playerHealth = 0;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            giveItem(globalValuesData.itemType.LUXURIES, 1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            giveItem(globalValuesData.itemType.JUNK, 1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            giveItem(globalValuesData.itemType.TRASH, 1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            giveItem(globalValuesData.itemType.FOOD, 1);
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            truckHealth = truckHealth - 100;
        }
    }

    public void giveItem(globalValuesData.itemType type, float amount)
    {
        if (amount != 0)
        {
            bool foundItemType = false;
            for (int i = 0; i < playerInventory.Count; i++)
            {
                if (playerInventory[i].entryType == type)
                {
                    foundItemType = true;
                    playerInventory[i] = new InventoryEntry { entryType = type, amountHeld = playerInventory[i].amountHeld + amount };
                }
            }
            if (foundItemType == false)
            {
                playerInventory.Add(new InventoryEntry { entryType = type, amountHeld = amount });
            }
        }
    }

    public float getItemAmount(globalValuesData.itemType type)
    {
        float amountToReturn = 0;
        foreach (InventoryEntry entry in playerInventory)
        {
            if (entry.entryType == type)
            {
                amountToReturn = entry.amountHeld;
            }
        }
        return amountToReturn;
    }

    public void setItemAmount(globalValuesData.itemType type, float setAmount)
    {
        List<InventoryEntry> tempList = new List<InventoryEntry>();
        tempList = playerInventory;
        for (int i = 0; i < tempList.Count; i++)
        {
            var entry = tempList[i];
            if (entry.entryType == type)
            {
                entry = new InventoryEntry { };
                entry.entryType = type;
                entry.amountHeld = setAmount;
                tempList[i] = entry;
                break;
            }
        }
        playerInventory = tempList;
    }

    public void reputationChange(int changeAmount, globalValuesData.factionType faction)
    {
        switch (faction)
        {
            case globalValuesData.factionType.BANDIT:
                banditRep = banditRep + changeAmount;
                freeTradeRep = freeTradeRep - (int)(changeAmount * 0.3f);
                corporationRep = corporationRep - (int)(changeAmount * 0.35f);
                break;

            case globalValuesData.factionType.FREETRADE:
                freeTradeRep = freeTradeRep + changeAmount;
                banditRep = banditRep - (int)(changeAmount * 0.2f);
                corporationRep = corporationRep - (int)(changeAmount * 0.5f);

                break;

            case globalValuesData.factionType.CORPORATION:
                corporationRep = corporationRep + changeAmount;
                banditRep = banditRep - (int)(changeAmount * 0.5f);
                freeTradeRep = freeTradeRep - (int)(changeAmount * 0.5f);

                break;

            case globalValuesData.factionType.FACTIONLESS:
                globalRep = globalRep + changeAmount;
                corporationRep = corporationRep + (int)(changeAmount * 0.1f);
                freeTradeRep = freeTradeRep + (int)(changeAmount * 0.3f);
                banditRep = banditRep + (int)(changeAmount * 0.5f);

                break;
        }

    }

    public void eatFood()
    {
        if (getItemAmount(globalValuesData.itemType.FOOD) > 0)
        {
            giveItem(globalValuesData.itemType.FOOD, gPTT.pR.foodRationSlider.value * gPTT.pR.playerEatRate);

            if (getItemAmount(globalValuesData.itemType.FOOD) < 0)
            {
                setItemAmount(globalValuesData.itemType.FOOD, 0);
            }
        }
    }

    public void healPlayer(float amount)
    {
        giveItem(globalValuesData.itemType.MEDSUPS, -1);
        gPTT.tS.populateInventoryTab();
        playerHealth = playerHealth + amount;
        Debug.Log("healed player for " + amount + " health");
    }

    public bool checkForItem(globalValuesData.itemType item)
    {
        if (getItemAmount(item) > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public float getFactionRep(globalValuesData.factionType faction)
    {
        float valueToReturn = 0;
        switch (faction)
        {
            case globalValuesData.factionType.BANDIT:
                valueToReturn = banditRep;
                break;

            case globalValuesData.factionType.CORPORATION:
                valueToReturn = corporationRep;
                break;

            case globalValuesData.factionType.FREETRADE:
                valueToReturn = freeTradeRep;
                break;

            case globalValuesData.factionType.FACTIONLESS:
                valueToReturn = globalRep;
                break;
        }
        return valueToReturn;
    }
}