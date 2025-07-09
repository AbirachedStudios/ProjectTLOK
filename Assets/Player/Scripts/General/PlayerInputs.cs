using UnityEngine;


    public class PlayerInputs : MonoBehaviour
    {
        public bool IsRunning { get; private set; }
        public bool IsJumping { get; private set; }
        public bool IsAttacking { get; private set; }
        public bool IsInteracting { get; private set; }
        public Vector3 MoveInput { get; private set; }

        void Update()
        {
            MoveInput = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            IsRunning = Input.GetKey(KeyCode.LeftShift);
            IsJumping = Input.GetKeyDown(KeyCode.Space);
            IsAttacking = Input.GetKey(KeyCode.Mouse0);
            IsInteracting = Input.GetKey(KeyCode.E);
        }
    }