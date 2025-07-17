using UnityEngine;

namespace PlayerData
{
public class PlayerInputs
{
    public bool IsRunning { get; private set; }
    public bool IsJumping { get; private set; }
    public bool IsAttacking { get; private set; }
    public bool IsInteracting { get; private set; }
    public Vector3 MoveInput { get; private set; }
    public void InputsUpdate()
    {
        MoveInput = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        IsRunning = Input.GetKey(KeyCode.LeftShift);
        IsJumping = Input.GetKey(KeyCode.Space);
        IsAttacking = Input.GetKeyDown(KeyCode.Mouse0);
        IsInteracting = Input.GetKeyDown(KeyCode.E);
    }
}
}