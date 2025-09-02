using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestItem : MonoBehaviour
{
    

    public ItemData itemData; // Reference to the Scriptable Object


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Inventory inventoryManager = FindObjectOfType<Inventory>();

            if (inventoryManager != null)
            {
                inventoryManager.AddItem(itemData);
                //PlayerController.instance.ChangeStats(category, boost, 5);
                Destroy(gameObject);
            }
        }
    }
}
