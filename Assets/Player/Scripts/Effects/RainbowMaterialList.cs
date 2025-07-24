using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainbowMaterialList : MonoBehaviour
{
    List<Material> teslaMaterial = new List<Material>();
    Renderer renderer;
    [SerializeField] float speed;
    float pepe;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        foreach (Material mat in renderer.materials)
        {
            mat.color = Color.HSVToRGB(pepe += 0.1f * speed * Time.deltaTime, 1.0f, 1.0f);
        }
        if (pepe >= 1.0f) pepe = 0f;
    }
}