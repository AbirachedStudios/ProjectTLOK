using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestItem : MonoBehaviour
{
    public int category;
    public float boost;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerController.instance.ChangeStats(category, boost, 5);
            Destroy(gameObject);
        }
    }
}
