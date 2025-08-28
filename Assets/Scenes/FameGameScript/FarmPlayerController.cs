using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Farm
{
    public class FarmPlayerController : MonoBehaviour
    {
        private Animator anim;

       //  private PlayerInput playerInput;

        private CharacterController cc;

        private Vector3 moveInput;

        public float currentSpeed = 0f;
        public float walkSpeed = 2f;
        public float RunSpeed = 5f;
        public float turnSpeed = 10f;

        private Vector3 velocity;
        private const float GRAVITY = -9.8f;

        public bool isRun;
        
        private void Start()
        {
            anim = GetComponent<Animator>();
            cc = GetComponent<CharacterController>();
        }

        private void OnMove(InputValue value)
        {
            var move = value.Get<Vector2>();
            moveInput = new Vector3(move.x, 0, move.y);
        }
        void Update()
        {
            velocity.y += GRAVITY;
            var dir = moveInput * currentSpeed + Vector3.up * velocity.y;


            cc.Move(dir * Time.deltaTime);

            Turn();

            SetAnimation();
        }

        private void Turn()
        {
            if(moveInput != Vector3.zero)
            {
                Quaternion targetRot = Quaternion.LookRotation(moveInput);
                transform.rotation = Quaternion.Slerp(transform.transform.rotation, targetRot, turnSpeed * Time.deltaTime);
            }
        }

        private void OnRun(InputValue value)
        {
            isRun = value.isPressed;
    
        }

        private void SetAnimation()
        {
            float targetValue = 0f;

            if (moveInput != Vector3.zero)
            { 
                targetValue = isRun ? 1f : 0.5f;
                currentSpeed = isRun ? RunSpeed : walkSpeed;
            }

            float animValue = anim.GetFloat("Vert");

            animValue = Mathf.Lerp(animValue, targetValue, 10f * Time.deltaTime);
            anim.SetFloat("Vert", animValue);
        }
    }
}
