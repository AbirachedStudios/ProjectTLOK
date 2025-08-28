using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity, IDestroyable, IDamageable
{
   public float life = 100;
   public void TakeDamage(float num)
   {
      life -= num;
   }

   public void DestroyByInterface()
   {
      
   }
}
