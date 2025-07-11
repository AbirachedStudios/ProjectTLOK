using UnityEngine;
    public class SoundControl : MonoBehaviour
    {
        private CharacterAudio ca;
        private float timer;
        private PlayerMovement pm;
        private CharacterController controller;
        private PlayerInputs playerInputs;
        private float stepTimer = 0f;
        private float intervaloPisadas = 0.5f;

        private void Start()
        {
            ca = GetComponent<CharacterAudio>();
            pm = GetComponent<PlayerMovement>();
            controller = GetComponent<CharacterController>();
            playerInputs = GetComponent<PlayerInputs>();
        }

        private void Update()
        {

            //if (timer > 0) { timer -= Time.deltaTime; }
            if (pm.isMoving && controller.isGrounded)
            {
                stepTimer -= Time.deltaTime;
                if (stepTimer <= 0f)
                {
                    Pisadas();
                    stepTimer = intervaloPisadas;
                }
                
            }
            //Sonido de salto
            if (Input.GetKeyDown(KeyCode.Space) && timer <= 0) { Salto(); }

            //Sonido de pisadas
            //if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) { Pisadas(); }
        }
        public void Pisadas()
        {
            AudioManager.instance.Steps(ca.pasos, transform.position);
        }

        public void Salto()
        {
            if (controller.isGrounded)
            {
                AudioManager.instance.PlaySound(ca.salto, transform.position);
                stepTimer = 0;
            }
           
        }
    }

