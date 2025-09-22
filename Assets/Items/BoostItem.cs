using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostItem : Item
{
    public int category;
    public float boost;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ItemEffect();
        }
    }

    public override void ItemEffect()
    {
        PlayerController.instance.ChangeStats(category, boost);
        Destroy(gameObject);
    }
}
