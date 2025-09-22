using UnityEngine;
using FMOD;
public class SoundControl
{
    CharacterAudio _ca;
    PlayerInputs _controller;
    CharacterController _cm;
    Transform _trans;
    float stepTimer = 0f;
    float intervaloPisadas = 0.5f;
    float timerJ;
    float timerA;
    bool busy;

    public SoundControl(Transform transform, CharacterAudio characterAudio, PlayerInputs playerInputs, CharacterController characterController)
    {
        _ca = characterAudio;
        _controller = playerInputs;
        _cm = characterController;
        _trans = transform;
    }

    public void SoundControllerUpdate()
    {
        if (timerJ > 0) { timerJ -= Time.deltaTime; }
        if (timerA > 0) { timerA -= Time.deltaTime; }

        if (timerJ <= 0 && timerA <= 0) { busy = false; }

        if (!busy)
        {
        
            //Sonido de pisadas
            if (_controller.MoveInput != Vector3.zero && _cm.isGrounded)
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

            //Sonido de ataque;
            Golpe();
        }

    }

    public void Pisadas()
    {
        if (_cm.isGrounded && _controller.MoveInput != Vector3.zero)
        {
            AudioManager.instance.Steps(_ca.pasos, _trans.position);
        }
    }

    public void Salto()
    {
        if (_controller.IsJumping && timerJ <= 0)
        {
            AudioManager.instance.PlaySound(_ca.salto, _trans.position);
            stepTimer = 0;
            timerJ = 1f;
            busy = true;
        }           
    }

    public void Golpe()
    {
        if (_controller.IsAttacking && timerA <= 0)
        {
            AudioManager.instance.PlaySound(_ca.golpe, _trans.position);
            stepTimer = 0;
            timerA = 1f;
            busy = true;
        }
    }
}

