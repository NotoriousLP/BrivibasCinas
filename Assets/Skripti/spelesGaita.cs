using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class spelesGaita : MonoBehaviour
{
    public Image[] states; // Array of Image components representing the states
    public Color playerColor = Color.green; // Color for the player's state
    public Color enemyColor = Color.red; // Color for the enemy's state

    void Start()
    {
        // Change the colors of all states in the array
        for (int i = 0; i < states.Length; i++)
        {
            // Assuming alternating states between player and enemy
            if (i == 0)
            {
                states[i].color = playerColor; // Set player color
            }
            else
            {
                states[i].color = enemyColor; // Set enemy color
            }
        }
    }
}
