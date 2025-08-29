using UnityEngine;
using PlayerData;
using FMOD;
public class SoundControl
    {
        CharacterAudio _ca;
        PlayerController _controller;
        CharacterController _cm;
        Transform _trans;
        float stepTimer = 0f;
        float intervaloPisadas = 0.5f;
        float timer;

    public SoundControl(Transform transform, CharacterAudio characterAudio, PlayerController playerController, CharacterController characterController)
    {
        _ca = characterAudio;
        _controller = playerController;
        _cm = characterController;
        _trans = transform;
    }

        public void SoundControllerUpdate()
        {
        if (timer > 0) { timer -= Time.deltaTime; }

        //Sonido de pisadas
        if (_controller.isMoving && _cm.isGrounded)
        {
            stepTimer -= Time.deltaTime;
            if (stepTimer <= 0f)
            {
                Pisadas(_trans.position);
                stepTimer = intervaloPisadas;
            }
        }

        //Sonido de salto
        Salto(_trans.position);
        }

        public void Pisadas(Vector3 position)
        {
            if (_cm.isGrounded && _controller.isMoving)
            {
                AudioManager.instance.Steps(_ca.pasos, position);
            }
        }

        public void Salto(Vector3 position)
        {
            if (_controller.isJumping && timer <= 0)
            {
                AudioManager.instance.PlaySound(_ca.salto, position);
                stepTimer = 0;
                timer = 1f;
            }
           
        }
    }

