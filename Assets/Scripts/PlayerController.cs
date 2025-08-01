using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.LightTransport;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class PlayerController : MonoBehaviour
{

    public Animator animator;

    public float walkSpeed = 3f;
    public float Damping = 20f;

    private Vector3 velocity;

    private float horizontal;
    private float vertical;

    private Vector3 inputDirection;


    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        inputDirection  = new Vector3(horizontal, 0, vertical);

        bool isMoving = Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0;
        float walkSpeed = 1f;

        animator.SetFloat("Speed", isMoving ? walkSpeed : 0f);
        PlayerRotate();
    }

    private void PlayerRotate()
    {
        if (inputDirection.sqrMagnitude > 0.0001f) // 0에 가까운 값 방지
        {
            inputDirection.Normalize();
            Quaternion targetRotation = Quaternion.LookRotation(inputDirection, Vector3.up);

            transform.GetChild(0).rotation = Quaternion.Slerp(transform.GetChild(0).rotation, targetRotation, Damping * Time.deltaTime);
        }

    }

    private void FixedUpdate()
    {
        // velocity = ((transform.forward * vertical) + (transform.right * horizontal)) * Time.fixedDeltaTime * walkSpeed;

        // 정규화해서 대각선 속도 보정
        if (inputDirection.sqrMagnitude > 1f)
            inputDirection.Normalize();

        velocity = inputDirection * walkSpeed * Time.fixedDeltaTime;
        transform.Translate(velocity, Space.World);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            other.gameObject.SetActive(false);
            Debug.Log("Player Hit!");
        }

    }
}


