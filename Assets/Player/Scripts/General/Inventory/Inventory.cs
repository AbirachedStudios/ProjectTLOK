using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject inventoryPanel; // Assign your InventoryPanel here in the Inspector
    private bool isInventoryOpen = false;
    public Text treasureCounterText;
    private int currentTreasureCount = 0; // Variable to store the treasure count


    void Start()
    {
        // Ensure the inventory is closed when the game starts
        if (inventoryPanel != null)
        {
            inventoryPanel.SetActive(false);
        }
        Time.timeScale = 1f; // Ensure game is unpaused at the start

        UpdateTreasureCounterDisplay();

    }

    void Update()
    {
        // Check for 'I' key press
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }
    }

    void ToggleInventory()
    {
        isInventoryOpen = !isInventoryOpen; // Toggle the state

        if (inventoryPanel != null)
        {
            inventoryPanel.SetActive(isInventoryOpen); // Show/hide the panel
        }

        // Pause/unpause the game
        if (isInventoryOpen)
        {
            Time.timeScale = 0f; // Pause the game
            //Cursor.lockState = CursorLockMode.None; // Unlock cursor
            //Cursor.visible = true; // Make cursor visible
        }
        else
        {
            Time.timeScale = 1f; // Unpause the game
            //Cursor.lockState = CursorLockMode.Locked; // Lock cursor to center
            //Cursor.visible = false; // Hide cursor
        }
    }

    public void AddTreasure(int amount)
    {
        currentTreasureCount += amount;
        UpdateTreasureCounterDisplay(); // Update the UI whenever treasure is added
        Debug.Log("Treasure collected! Current count: " + currentTreasureCount);
    }

    /// <summary>
    /// Updates the UI Text element to display the current treasure count.
    /// </summary>
    void UpdateTreasureCounterDisplay()
    {
        if (treasureCounterText != null)
        {
            treasureCounterText.text = "Treasure: " + currentTreasureCount;
        }
    }

}
