using UnityEditorInternal;
using UnityEngine;

public class TestCar : MonoBehaviour
{
    float moveSpeed = 3f;
    private Rigidbody2D rb;
    private float inputX;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        inputX = Input.GetAxisRaw("Horizontal");

        //position+�̵����
        //transform.position += Vector3.right * inputX * moveSpeed * Time.deltaTime;

        //Translate�̵����
        //Vector3 velocity = (transform.right * inputX) * Time.fixedDeltaTime * moveSpeed;
        //transform.Translate(velocity, Space.World);

        //Transform �̵������ �浹�� �����߻�
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(inputX * moveSpeed, rb.linearVelocity.y);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Finish")
        {
            Debug.Log("Wall Col!");
        }
        
    }
    
}
