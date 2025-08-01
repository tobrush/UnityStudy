using UnityEditor;
using UnityEditor.Overlays;
using UnityEngine;

public class FPSPlayerController : MonoBehaviour
{

    

    private CharacterController cc;

    private float gravity = -10f;
    private float yVelocity = 0f;

    public Camera cam;

    public float rotSpeed = 200f;
    public float moveSpeed = 10;

    public float jumpPower = 10f;
    bool isJumping = false;

    private float mx = 0f;
    private float my = 0f;


    public GameObject firePosition;
    public GameObject bombFactory;
    public float throwPower = 15f;
    private GameObject reloadBomb;

    public GameObject bulletEffect;
    private ParticleSystem ps;

    public int weaponPower = 5;

    public Animator anim;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        cc = GetComponent<CharacterController>();
        ps = bulletEffect.GetComponent<ParticleSystem>();

    }

    //

    public int hp = 20;

    public void DamageAction(int damage)
    {
        hp = hp - damage;
    }

    private void Update()
    {


        //Key move
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(h, 0, v);

        dir = dir.normalized;
        dir = Camera.main.transform.TransformDirection(dir);


        yVelocity += gravity * Time.deltaTime;
        dir.y = yVelocity;

        cc.Move(dir * moveSpeed * Time.deltaTime);

        //Mouse rotate
        float mouse_X = Input.GetAxis("Mouse X");
        float mouse_Y = Input.GetAxis("Mouse Y");

        mx += mouse_X * rotSpeed * Time.deltaTime;
        my += mouse_Y * rotSpeed * Time.deltaTime;

        my = Mathf.Clamp(my, -90f, 90f);

        cam.transform.eulerAngles = new Vector3(-my, mx, 0);

        transform.eulerAngles = new Vector3(0, mx, 0);

        anim.SetFloat("MoveMotion", dir.magnitude);

        //collisionFlags 
        //None = 무접촉
        //Sides = 옆면 접촉
        //Above = 머리 접촉
        //Below = 아래 접촉

        if (cc.collisionFlags == CollisionFlags.Below)
        {
            if (isJumping)
            {
                isJumping = false;
               
            }
            yVelocity = 0;
        }

        //Jump
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            yVelocity = jumpPower;
            isJumping = true;
        }
        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 버튼 클릭
        {
            anim.SetTrigger("shoot");
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            RaycastHit hitInfo = new RaycastHit();

            if (Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Enemy")) // Raycast를 Enemy가 맞은 경우
                {
                    EnemyFSM eFSM = hitInfo.transform.GetComponent<EnemyFSM>();
                    eFSM.HitEnemy(weaponPower);
                }
                else // Raycast를 맞은 대상이 Enemy가 아닌 경우
                {
                   // bulletEffect.transform.position = hitInfo.point;
                   // bulletEffect.transform.forward = hitInfo.normal;

                   // ps.Play();

                    GameObject Bullet = Instantiate(bulletEffect);
                    Bullet.transform.forward = hitInfo.normal;

                    Bullet.transform.position = hitInfo.point;
                }


               

        
            }
        }


        if (Input.GetMouseButtonDown(1)) // 마우스 오른쪽 버튼 클릭
        {
            

            GameObject bomb = Instantiate(bombFactory, cam.transform);
            bomb.transform.position = firePosition.transform.position;
           
            reloadBomb = bomb;

            Rigidbody rb = reloadBomb.GetComponent<Rigidbody>();
            rb.useGravity = false;
        }

        if (Input.GetMouseButtonUp(1)) // 마우스 오른쪽 버튼 클릭
        {
            reloadBomb.transform.SetParent(null);
            Rigidbody rb = reloadBomb.GetComponent<Rigidbody>();
            rb.useGravity = true;
            rb.AddForce(Camera.main.transform.forward * throwPower, ForceMode.Impulse);
        }
    }
}
