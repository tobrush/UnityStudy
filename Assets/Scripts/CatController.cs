using Unity.VisualScripting;
using UnityEngine;

public class CatController : MonoBehaviour
{
    private Rigidbody2D catRb;

    public float jumpSpeed = 50f;
    private Animator catAnim;

    public GameObject catSprite;

    public int jumpCount = 0;
    public float jumpPower = 30f;
    public float limitPower = 25f;

    public SoundManager soundManager;

    public GameObject gameOverUI;
    public GameObject fadeUI;


    public GameObject happyVideo;
    public GameObject sadVideo;


    public bool isGround = false;

    public GameObject GameOver;
    void Start()
    {
        catRb = GetComponent<Rigidbody2D>();
        catAnim = catSprite.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 10)
        {
            catAnim.SetTrigger("Jump");
            catAnim.SetBool("isGround", false);
            jumpCount++; // 1씩 증가
            soundManager.OnJumpSound();
            catRb.AddForceY(jumpPower, ForceMode2D.Impulse);

            if (catRb.linearVelocityY > limitPower) // 자연스러운 점프를 위한 속도 제한
                catRb.linearVelocityY = limitPower;
        }

        var catRotation = transform.eulerAngles;
        catRotation.z = catRb.linearVelocityY * 2.5f;
        transform.eulerAngles = catRotation;

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Pipe")) // 파이프에 부딪혀서 실패
        {
            soundManager.OnColliderSound();

            gameOverUI.SetActive(true); // 게임 오버 켜기
            fadeUI.SetActive(true); // 페이드 켜기
            fadeUI.GetComponent<FadeRoutine>().OnFade(3f, Color.black); // 페이드 실행
            this.GetComponent<CircleCollider2D>().enabled = false;

            Invoke("SadVideo", 5f);
        }
        if (other.gameObject.CompareTag("Apple"))
        {
            other.gameObject.SetActive(false);

            other.transform.parent.GetComponent<ItemEvent>().particle.SetActive(true);

            CatGameManager.score++;
            //CatGameManager.Instance.score++;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag ("Ground"))
        {
            catAnim.SetBool("isGround", true);
            jumpCount = 0;
            isGround = true;
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGround = false;
        }
    }
}


