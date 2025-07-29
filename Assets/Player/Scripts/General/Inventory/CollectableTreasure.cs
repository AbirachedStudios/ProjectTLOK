using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableTreasure : MonoBehaviour
{
    public int treasureValue = 1; // How much treasure this object gives

    void OnTriggerEnter(Collider other) // Or OnCollisionEnter, depending on your setup
    {
        // Check if the object that entered the trigger is the player
        if (other.CompareTag("Player")) // Make sure your player GameObject has the "Player" tag
        {
            // Find the InventoryManager in the scene
            Inventory inventoryManager = FindObjectOfType<Inventory>();

            if (inventoryManager != null)
            {
                inventoryManager.AddTreasure(treasureValue); // Add the treasure
                Destroy(gameObject); // Destroy the collectable object after picking it up
            }
            else
            {
                Debug.LogError("InventoryManager not found in the scene!");
            }
        }
    }
}
