using SimpleInputNamespace;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class KnightController_Keyboard : MonoBehaviour
{
    public JoystickController joystickController;

    public enum AnimState { idle, run, jump, fall, crouch, crouchWlak, wallSlide, attack1, attack2, attack3, crouchAttack, dash }
    public AnimState animState = AnimState.idle;

    private Animator animator;
    private Rigidbody2D knightRb;

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float jumpPower = 7f;

    [Header("Ground Check & Wall Check")]
    public LayerMask groundLayer;

    public Transform groundCheckOrigin; // 발 밑 오브젝트 확인
    public float groundCheckRadius = 0.1f;
    public bool isGrounded;

    public Transform topCheckOrigin; // 머리 위 오브젝트 확인
    public float TopCheckRadius = 0.1f;
    private bool isTop;

    public Transform wallCheckOrigin; //  벽 체크용
    public Vector2 boxSize = new Vector2(0.5f, 0.5f);
    public bool isTouchingWall;
    public bool isWallSliding;

    public bool Crouched = false;

    public bool oneTime = false; // Fall Anim

    private Vector3 inputDir;


    [Header("Attack Combo")]
    public int attackStep = 0;
    public bool isAttacking = false;
    public bool canCombo = false;
    public bool comboQueued = false;

    private bool stopInput = false;

    Coroutine attackCoroutine;

    [Header("Dash Settings")]
    [SerializeField] private float dashPower = 12f;
    [SerializeField] private float dashUp = 2;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float dashCooldown = 1f;

    public bool isDashing = false;
    private float lastDashTime = -999f;


void Start()
    {
        animator = this.transform.GetChild(0).GetComponent<Animator>();
        knightRb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(joystickController.UseJoystick)
        {
            InputTouch();
        }
        else
        {
            InputKeyboard();
        }
            
            UpdateAnimationState();
    }

    bool oneShort = true;
    void InputTouch()
    {
        inputDir = joystickController.movePos;

        if (inputDir.y < -0.5f)
        {
            if (isGrounded)
            {
                if (!Crouched)
                {
                    scaleColliderBox(true);
                }
            }
        }

        if(inputDir.y > -0.3f && inputDir.y < 0.3f)
        {
            if (isGrounded)
            {
                if (Crouched)
                {
                    scaleColliderBox(false);
                }
            }
        }
      
        if (inputDir.y > 0.5f)
        {
            if(oneShort)
            {
                Jump();
                Debug.Log("JoystickJump");
               
                oneShort = false;

                Invoke("JumpReload", 1.0f);
            }
           
        }

    }

    void JumpReload()
    {
        oneShort = true;
    }

    /*
    public void InputJoystick(float x, float y)
    {
        inputDir = new Vector3(x, y, 0);
    }
    */

    void InputKeyboard()
    {
        if (stopInput) return;

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        inputDir = new Vector3(h, v, 0);




        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Crouch();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            if (isGrounded)
            {
                if (!Crouched)
                {
                    scaleColliderBox(true);
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            if (isGrounded)
            {
                if (Crouched)
                {
                    scaleColliderBox(false);
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Attack();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            TryDash();
        }

    }

    public void TryDash()
    {
        if (Time.time >= lastDashTime + dashCooldown && !isDashing && !isAttacking)
        {
            StartCoroutine(Dash());
        }
        
    }
    IEnumerator Dash()
    {
        isDashing = true;
        lastDashTime = Time.time;
        stopInput = true;

        animState = AnimState.dash; 
        PlayAnimation();

       // this.gameObject.GetComponent<CapsuleCollider2D>().isTrigger = true;
        this.gameObject.GetComponent<CapsuleCollider2D>().offset = new Vector2(0f, -0.7f);
        this.gameObject.GetComponent<CapsuleCollider2D>().size = new Vector2(0.8f, 0.6f);


        float dashDir = Mathf.Sign(transform.localScale.x);
        knightRb.linearVelocity = new Vector2(dashDir * dashPower, dashUp);

        yield return new WaitForSeconds(dashDuration *0.5f);

       // this.gameObject.GetComponent<CapsuleCollider2D>().isTrigger = false;
        this.gameObject.GetComponent<CapsuleCollider2D>().offset = new Vector2(0f, -0.9f);
        this.gameObject.GetComponent<CapsuleCollider2D>().size = new Vector2(0.8f, 1.6f);
        
        yield return new WaitForSeconds(dashDuration * 0.5f);

        

        knightRb.linearVelocity = new Vector2(0f, knightRb.linearVelocity.y);
        stopInput = false;
        isDashing = false;
    }
    public void Jump()
    {
        if (isGrounded)
        {

            if (!isTop && Crouched)
            {
                scaleColliderBox(false);

            }
            //knightRb.linearVelocity = new Vector2(knightRb.linearVelocity.x, jumpPower);
            knightRb.AddForceY(jumpPower, ForceMode2D.Impulse);
            animState = AnimState.jump;
        }

        if (isWallSliding && !Crouched)
        {
            if (!isTop && Crouched)
            {
                scaleColliderBox(false);
            }

            int powerDir = this.transform.localScale.x > 0 ? -1 : 1;

            //knightRb.linearVelocity = new Vector2(knightRb.linearVelocity.x, jumpPower);

            knightRb.AddForce(new Vector2((jumpPower * 0.3f) * powerDir, jumpPower), ForceMode2D.Impulse);
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            animState = AnimState.jump;
            PlayAnimation();
            StartCoroutine(JustStopInput(0.3f));
        }

    }

    void scaleColliderBox(bool small)
    {
        if (small)
        {
            Crouched = true;
            moveSpeed = 2;

            this.gameObject.GetComponent<CapsuleCollider2D>().offset = new Vector2(0f, -1.3f);
            this.gameObject.GetComponent<CapsuleCollider2D>().size = new Vector2(0.8f, 0.8f);
        }
        else
        {
            Crouched = false;
            moveSpeed = 4;

            this.gameObject.GetComponent<CapsuleCollider2D>().offset = new Vector2(0f, -0.9f);
            this.gameObject.GetComponent<CapsuleCollider2D>().size = new Vector2(0.8f, 1.6f);
        }
    }

    public void Crouch()
    {
        if (isGrounded)
        {
            if (!Crouched)
            {
                scaleColliderBox(true);
            }
            else
            {
                if (!isTop)
                {
                    scaleColliderBox(false);
                }
            }
        }
    }





    public void Attack()
    {
        if (isGrounded)
        {
            if (Crouched)
            {
                if (attackCoroutine == null)
                    attackCoroutine = StartCoroutine(PlayCrouchAttack());

                return;

            }
            else
            {
                // 서 있을 때만 콤보 공격
                if (!isAttacking)
                {
                    attackStep = 1;
                    StartCoroutine(PlayAttackCombo(attackStep));
                }
                else if (canCombo)
                {
                    comboQueued = true;
                }
            }
        }
    }

    IEnumerator JustStopInput(float time)
    {
        stopInput = true;
        yield return new WaitForSeconds(time);
       
        // 앉은 상태에서는 단일 공격
        stopInput = false;
    }

    IEnumerator PlayCrouchAttack()
    {
        
        isAttacking = true;
        animState = AnimState.crouchAttack;
        PlayAnimation(); // 바로 실행
        yield return new WaitForSeconds(0.5f);
        // 앉은 상태에서는 단일 공격
        isAttacking = false;

        attackCoroutine = null; // 종료 후 초기화
    }

    IEnumerator PlayAttackCombo(int step)
    {
        isAttacking = true;
        canCombo = false;
        comboQueued = false;

        switch (step)
        {
            case 1: animState = AnimState.attack1; break;
            case 2: animState = AnimState.attack2; break;
            case 3: animState = AnimState.attack3;
                StartCoroutine(JustStopInput(0.7f)); break;
        }

        PlayAnimation();

        yield return new WaitForSeconds(0.2f); // 콤보 입력 타이밍
        canCombo = true;

        float waitTime = 0.4f;
        float timer = 0f;
        while (timer < waitTime)
        {
            if (comboQueued) break;
            timer += Time.deltaTime;
            yield return null;
        }

        canCombo = false;

        if (comboQueued && step < 3)
        {
            attackStep++;
            StartCoroutine(PlayAttackCombo(attackStep));
        }
        else
        {
            attackStep = 0;
            isAttacking = false;
        }
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        if (stopInput) return;

        if (isDashing) return;

        if (isAttacking)
        {
            knightRb.linearVelocity = Vector2.zero;
            return;
        }


        if (inputDir.x != 0)
        {
            // 이동 처리
            knightRb.linearVelocityX = inputDir.x * moveSpeed;

            // 좌우 방향 전환
            transform.localScale = new Vector3(Mathf.Sign(inputDir.x), 1f, 1f);
            
            
        }
        if(!isWallSliding)
        {
            // 입력 없을 때 x속도 강제로 0 처리
            if (Mathf.Approximately(inputDir.x, 0f) && isGrounded)
            {
                knightRb.linearVelocity = new Vector2(0f, knightRb.linearVelocity.y);
            }
        }
       

        // 바닥 감지
        CheckGround();
    }

    void CheckGround()
    {
        // 바닥, 벽, 천장 체크
        isGrounded = CheckGrounded();
        isTouchingWall = CheckWall();
        isTop = CheckTop();

        // 벽 슬라이드 조건: 공중 + 벽에 닿았을 때
        isWallSliding = !isGrounded && isTouchingWall && knightRb.linearVelocity.y < 0;

        // 벽 슬라이드 중이면 낙하속도 제한
        if (isWallSliding && !Crouched)
        {
            this.transform.GetChild(0).localPosition = new Vector2(-0.08f, 0);
            knightRb.linearVelocity = new Vector2(knightRb.linearVelocity.x, Mathf.Clamp(knightRb.linearVelocity.y, -1f, float.MaxValue));

        }
        else
        {
            this.transform.GetChild(0).localPosition = new Vector2(0.2f, 0);
        }
    }


    bool CheckGrounded()
    {
        return Physics2D.OverlapCircle(groundCheckOrigin.position, groundCheckRadius, groundLayer);
    }

    bool CheckTop()
    {
        return Physics2D.OverlapCircle(topCheckOrigin.position, TopCheckRadius, groundLayer);
    }

    bool CheckWall() 
    {
        return Physics2D.OverlapBox(wallCheckOrigin.position, boxSize, 0f, groundLayer);
    }


    void UpdateAnimationState()
    {
        if (isDashing) return;

        if (isAttacking) return;

        if (!isGrounded) // 공중
        {

            if (!isWallSliding) // 일반
            {
                if (knightRb.linearVelocity.y > 0.1f)
                {
                    animState = AnimState.jump;
                }

                else if (knightRb.linearVelocity.y < 0f)
                    animState = AnimState.fall;
            }
            else // 벽 붙음
            {
                if (!Crouched)
                {


                    if (knightRb.linearVelocity.y > 0.1f)
                    {
                        animState = AnimState.jump;
                    }
                    else if (knightRb.linearVelocity.y < -0.1f)
                        animState = AnimState.wallSlide;
                }
            }

        }
        else // 지상
        {
            if (!Crouched) // 서있는다면?
            {
                if (Mathf.Abs(inputDir.x) > 0.01f)
                    animState = AnimState.run;
                else
                    animState = AnimState.idle;
            }
            else // 앉는다면?
            {
                if (Mathf.Abs(inputDir.x) > 0.01f)
                    animState = AnimState.crouchWlak;
                else
                    animState = AnimState.crouch;
            }


        }

        PlayAnimation();
    }
    void PlayAnimation()
    {
       

        switch (animState)
        {
            case AnimState.idle:
                animator.Play("_Idle");
                StopCoroutine(PlayAttackCombo(1));
                break;
            case AnimState.run:
                animator.Play("_Run");
                break;
            case AnimState.jump:
                oneTime = true;
                animator.Play("_Jump");
                break;
            case AnimState.fall:
                if (oneTime)
                {
                    animator.Play("_JumpFallInbetween");
                    oneTime = false;
                }
                break;

            case AnimState.crouch:
                animator.Play("_Crouch");
                StopCoroutine(PlayCrouchAttack());
                break;
            case AnimState.crouchWlak:
                animator.Play("_CrouchWalk");
                break;

            case AnimState.wallSlide:
                oneTime = true;
                animator.Play("_WallSlide");
                break;

            case AnimState.attack1:
                animator.Play("_Attack");
                break;
            case AnimState.attack2:
                animator.Play("_Attack2");
                break;
            case AnimState.attack3:
                animator.Play("_AttackCombo");
                break;
            case AnimState.crouchAttack:
                animator.Play("_CrouchAttack");
                break;
            case AnimState.dash:
                animator.Play("_Roll"); // ← 애니메이션 클립 이름에 따라 수정
                break;
        }
    }
    
    // 씬에서 레이 확인용
    void OnDrawGizmos()
    {
        if (groundCheckOrigin != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheckOrigin.position, groundCheckRadius);
        }

        if (topCheckOrigin != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(topCheckOrigin.position, TopCheckRadius);
        }

        if (wallCheckOrigin != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(wallCheckOrigin.position, boxSize);
        }
    }
   
}
