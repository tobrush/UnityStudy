using UnityEngine;
using UnityEngine.InputSystem;

public class TownMovement : MonoBehaviour
{
    Rigidbody2D rigid2D;
    SpriteRenderer spriteRenderer;
    Animator animator;

    public Vector2 moveVector;


    [SerializeField]
    float speed = 3f;

    private void Awake()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

    }

    private void FixedUpdate()
    {
        Vector2 nextVector = moveVector.normalized * speed * Time.fixedDeltaTime;
        rigid2D.MovePosition(rigid2D.position + nextVector);

        if (moveVector != Vector2.zero)
        {
            animator.SetBool("isWalking", true);
            //좌우반전
            transform.localScale = new Vector3(Mathf.Sign(moveVector.x), 1f, 1f);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }
    /*
    private void LateUpdate()
    {
        //up, dwon, L, R 이 없는 경우, 좌우 플립
        if(moveVector.x !=0)
        {
            spriteRenderer.flipX = movementVector.x < 0;
        }
    }
    */

    private void OnMove(InputValue value)
    {
        moveVector = value.Get<Vector2>();

        if (moveVector != Vector2.zero)
        {
            animator.SetFloat("Horizontal", moveVector.x);
            animator.SetFloat("Vertical", moveVector.y);
        }

    }

 
}
