using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner// : MonoBehaviour
{
    Item drop;

    public Item DropItem(List<Item> items)
    {
        float item = Random.Range(0,100);
        switch (item)
        {
            case < 20:
                drop = items[0];
                /*if (drop != null) 
                {
                    Instantiate(drop, transform.position, transform.rotation);
                }*/
                break;

            case < 30:
                drop = items[1]; 
                /*if (drop != null)
                {
                    Instantiate(drop, transform.position, transform.rotation);
                }*/
                break;

            case < 60:
                drop = items[2]; 
                /*if (drop != null)
                {
                    Instantiate(drop, transform.position, transform.rotation);
                }*/
                break;

            default:
                break;
        }

        return drop;
    }
}
