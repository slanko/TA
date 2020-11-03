using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerResources : MonoBehaviour
{
    public float playerHealth, truckHealth, playerCredit, playerLuck;

    //FACTION REP
    public int banditRep, freeTradeRep, corporationRep, globalRep;


    [SerializeField] Slider healthBar, foodRationSlider, truckSpeedSlider;
    bool playerIsAlive = true;
    [SerializeField] Animator deathScreen;
    [SerializeField] Text creditsText, foodRationText, truckSpeedText;
    GameObject GOD;
    godPointToThing gPTT;

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
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = playerHealth;
        float foodRationValue = 1;

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

        if(gPTT.PLAYER.currentState == playerScript.playerState.TRAVELLING)
        {
            playerHealth = playerHealth - .25f * Time.deltaTime * foodRationValue;
        }

        if(playerHealth <= 0 && playerIsAlive)
        {
            //game OVERRRRRRRRRRRRR
            Debug.Log("you fricking died");
            deathScreen.SetTrigger("die");
            playerIsAlive = false;
        }
        if(playerHealth > 1000)
        {
            playerHealth = 1000;
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
        if(amount != 0)
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
}