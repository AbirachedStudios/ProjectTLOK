using System;
using UnityEngine;
using FMOD;

[Serializable]
public class SoundControl
{
    private CharacterAudio _ca;
    private PlayerController _controller;
    private CharacterController _cm;
    public Transform _trans;
    private float stepTimer = 0f;
    private float intervaloPisadas = 0.5f;
    private float timer;

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

