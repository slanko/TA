using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerResources : MonoBehaviour
{
    public float playerHealth;
    [SerializeField] Slider healthBar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerHealth <= 0)
        {
            //game OVERRRRRRRRRRRRR
            Debug.Log("you fricking died");
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            playerHealth = 0;
        }
    }

    public void loseHealth(float healthLost)
    {
        playerHealth = playerHealth - healthLost;
    }
}
