using UnityEngine;

public class Enemy : Entity, IDestroyable
{
   [SerializeField] private GameObject hitParticle;
   
   
   public float life = 100;
   public void TakeDamage(float num)
   {
      life -= num;
      Instantiate(hitParticle, transform.position, Quaternion.identity);
   }

   public Transform damageableTransform { get; set; }

   public void DestroyByInterface()
   {
      
   }
}
