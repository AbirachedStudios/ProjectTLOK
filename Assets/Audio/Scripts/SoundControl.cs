using UnityEngine;
using PlayerData;
    public class SoundControl : MonoBehaviour
    {
        private CharacterAudio ca;
        private float timer;
        private PlayerController controller;
        private CharacterController cm;
        private float stepTimer = 0f;
        private float intervaloPisadas = 0.5f;
        
        private void Start()
        {
            ca = GetComponent<CharacterAudio>();
            cm = GetComponent<CharacterController>();
            controller = GetComponent<PlayerController>();
        }

        private void Update()
        {



        if (timer > 0) { timer -= Time.deltaTime; }
        if (controller.isMoving && cm.isGrounded)
        {
            stepTimer -= Time.deltaTime;
            if (stepTimer <= 0f)
            {
                Pisadas();
                stepTimer = intervaloPisadas;
            }
        }
        //Sonido de salto
        Salto();
            //if (controller.isJumping && timer <= 0) { Salto(); }

        //Sonido de pisadas
        //Pisadas();
        }
        public void Pisadas()
        {
            if (cm.isGrounded && controller.isMoving)
            {
                AudioManager.instance.Steps(ca.pasos, transform.position);
            }
        }

        public void Salto()
        {
            if (controller.isJumping && timer <= 0)
            {
                AudioManager.instance.PlaySound(ca.salto, transform.position);
                stepTimer = 0;
                timer = 1f;
            }
           
        }
    }

