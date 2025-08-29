using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public void TakeDamage(float num);
    public Transform damageableTransform { get; set; }
}
