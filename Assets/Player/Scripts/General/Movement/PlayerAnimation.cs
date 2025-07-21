
using UnityEngine;
using PlayerData;
using UnityEngine.Windows;
using System;


public class PlayerAnimation
{
    PlayerInputs _pInputs;
    CharacterController _ccontroller;
    Animator _animator;
    public PlayerAnimation(PlayerInputs pInputs, CharacterController controller, Animator animator)
    {
        _pInputs = pInputs;
        _ccontroller = controller;
        _animator = animator;
    }

    public void AnimationsUpdate()
    {
        if (_pInputs.IsAttacking)
        {
            _animator.SetTrigger("Attack");
            int combo = _animator.GetInteger("Combo");
            _animator.SetInteger("Combo", combo + 1);
        }
        if ((_ccontroller.isGrounded) && (_pInputs.IsJumping))
        {
            _animator.SetTrigger("Jump");
            Debug.Log("Salte");
        }
      /*  else
        {
            _animator.ResetTrigger("Jump");
        }*/
    }

}
