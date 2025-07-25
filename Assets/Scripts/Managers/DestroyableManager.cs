using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableManager : MonoBehaviour
{
    [SerializeField] List<IDestroyable> destroyables = new List<IDestroyable>();
    public void DestroyByInterface(IDestroyable destroyable)
    {
        if (destroyables.Contains(destroyable))
        {
            destroyables.Remove(destroyable);
            Destroy(destroyable as MonoBehaviour);
        }
    }
}
    