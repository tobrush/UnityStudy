using System;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private Rigidbody2D characterRb;
    public SpriteRenderer[] renderers;

    public float moveSpeed;
    public float jumpPower = 10f;
    private float h;

    public bool isGround;

    void Start()
    {
        characterRb = GetComponent<Rigidbody2D>();

        renderers = GetComponentsInChildren<SpriteRenderer>(true);
    }

    void Update()
    {
        h = Input.GetAxis("Horizontal"); // 키 입력

        Jump();
    }

    void FixedUpdate()
    {
        Move();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        isGround = true;

        renderers[2].gameObject.SetActive(false); // Jump
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        isGround = false;

        renderers[0].gameObject.SetActive(false); // Idle
        renderers[1].gameObject.SetActive(false); // Run
        renderers[2].gameObject.SetActive(true); // Jump
    }

    /// <summary>
    /// 캐릭터 움직임에 따라 이미지의 Flip 상태가 변하는 기능
    /// </summary>
    private void Move()
    {
        if (!isGround)
            return;

        if (h != 0) // 입력 키를 누를 때
        {
            renderers[0].gameObject.SetActive(false); // Idle
            renderers[1].gameObject.SetActive(true); // Run

            characterRb.linearVelocityX = h * moveSpeed; // 물리적인 이동

            if (h > 0)
            {
                renderers[0].flipX = false;
                renderers[1].flipX = false;
                renderers[2].flipX = false;
            }
            else if (h < 0)
            {
                renderers[0].flipX = true;
                renderers[1].flipX = true;
                renderers[2].flipX = true;
            }
        }
        else if (h == 0)// 움직이지 않을 때
        {
            renderers[0].gameObject.SetActive(true); // Idle
            renderers[1].gameObject.SetActive(false); // Run
        }
    }

    /// <summary>
    /// 캐릭터가 +Y 방향으로 점프하는 기능
    /// </summary>
    private void Jump()
    {
        if (Input.GetButtonDown("Jump")) // Input.GetKeyDown(KeyCode.Space)
        {
            characterRb.AddForceY(jumpPower, ForceMode2D.Impulse);
        }
    }
}