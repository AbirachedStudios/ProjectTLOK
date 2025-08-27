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
    private ItemData[] items; // Array to hold the actual item data

    public GameObject[] inventorySlots; // The UI squares in your inventory

   
    void Start()
    {
        // Ensure the inventory is closed when the game starts
        if (inventoryPanel != null)
        {
            inventoryPanel.SetActive(false);
        }
        Time.timeScale = 1f; // Ensure game is unpaused at the start

        UpdateTreasureCounterDisplay();

        items = new ItemData[inventorySlots.Length];

        UpdateAllSlotsUI();
        ConnectButtonsToSlots();

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
            Cursor.lockState = CursorLockMode.None; // Unlock cursor
            Cursor.visible = true; // Make cursor visible
        }
        else
        {
            Time.timeScale = 1f; // Unpause the game
            Cursor.lockState = CursorLockMode.Locked; // Lock cursor to center
            Cursor.visible = false; // Hide cursor
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

    public void AddItem(ItemData itemToAdd)
    {
        // Find an empty slot
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null)
            {
                // Assign the item data to the empty slot
                items[i] = itemToAdd;

                // Get the child Image component from the UI slot
                Image itemIcon = inventorySlots[i].transform.GetComponent<Image>();

                // Set the sprite and make the icon visible
                itemIcon.sprite = itemToAdd.itemIcon;
                itemIcon.color = new Color(1, 1, 1, 1); // Make it fully visible

                Debug.Log("Item '" + itemToAdd.itemName + "' added to inventory!");
                return; // Exit the loop after finding a slot
            }
        }

        // If the loop finishes, the inventory is full
        Debug.LogWarning("Inventory is full!");
    }

    // Helper method to clear all slots at the start
    void UpdateAllSlotsUI()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            Image itemIcon = inventorySlots[i].transform.GetComponent<Image>();
            itemIcon.sprite = null;
            itemIcon.color = new Color(1, 1, 1, 0); // Hide the icon
        }
    }

    public void UseItem(int slotIndex)
    {
        if (items[slotIndex] != null)
        {
            ItemData itemToUse = items[slotIndex];

            // Use a switch statement to check the item's name and perform a different action.
            switch (itemToUse.itemName)
            {
                case "Buff":
                    PlayerController.instance.ChangeStats(0, 10f);
                    break;
            }

            // Remove the item from the inventory after it's used
            RemoveItem(slotIndex);
        }
    }

    /// <summary>
    /// Removes an item from the inventory.
    /// </summary>
    /// <param name="slotIndex">The index of the slot to remove the item from.</param>
    public void RemoveItem(int slotIndex)
    {
        items[slotIndex] = null; // Clear the item data

        // Update the UI to reflect the change
        Image itemIcon = inventorySlots[slotIndex].transform.GetComponent<Image>();
        itemIcon.sprite = null;
        itemIcon.color = new Color(1, 1, 1, 0); // Hide the icon
    }

    // This connects each button's OnClick() event to the UseItem() method.
    // We can't do this in the Inspector because we need to pass a parameter (the slot's index).
    private void ConnectButtonsToSlots()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            Button button = inventorySlots[i].GetComponent<Button>();
            int slotIndex = i; // Create a local copy of the loop variable

            button.onClick.AddListener(() => UseItem(slotIndex));
        }
    }
}