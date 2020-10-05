using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerResources : MonoBehaviour
{
    public float playerHealth, truckHealth, playerCredit, playerLuck;
    [SerializeField] Slider healthBar;
    bool playerIsAlive = true;
    [SerializeField] Animator deathScreen;
    [SerializeField] Text creditsText;

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
    }
}
