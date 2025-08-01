using System;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Turret : MonoBehaviour
{

    public GameObject bullet;

    public GameObject feelText;
    public Transform turretHead;
    public Transform turretBody;
    public Transform targetPlayer;
    // Update is called once per frame

    public float Distance = 5.0f;

    public SpriteRenderer AttackArea;
    public SpriteRenderer DetectArea;

    private float timer = 0f;
    public float interval = 2f;

    private Vector3 moveDirection;
    private float moveTimer;
    public float moveSpeed = 2f;
    public float moveChangeInterval = 2f;
    public float detectionRange = 8f;

    public bool moving = false;

    public bool headRotate = false;
    private void Start()
    {
        PickNewMoveDirection();

        AttackArea.transform.localScale = new Vector3(Distance * 2, Distance * 2, Distance * 2);
        DetectArea.transform.localScale = new Vector3(detectionRange * 2, detectionRange * 2, detectionRange * 2);

        if (turretHead == null)
        {
            turretHead = transform.GetChild(0);
        
        }

        if(targetPlayer == null)
        {
            targetPlayer = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void PickNewMoveDirection()
    {
        // 수평 평면에서 랜덤 방향 선택
        float angle = UnityEngine.Random.Range(0f, 360f);
        moveDirection = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)).normalized;
    }

    void Update()
    {
        if(moving)
        {
            moveRandom();
        }
        

        float dist = Vector3.Distance(turretHead.transform.position, targetPlayer.transform.position);

        if (turretHead != null & targetPlayer != null && dist < Distance)
        {
           
            Vector3 ignoreY = new Vector3(targetPlayer.transform.position.x, turretHead.transform.position.y, targetPlayer.transform.position.z);

            Vector3 direction = (ignoreY - turretHead.position).normalized;
            // 목표 회전 계산
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            float rotationSpeed = 5f; // 속도 조절 가능
            turretHead.rotation = Quaternion.Slerp(turretHead.rotation, targetRotation, Time.deltaTime * rotationSpeed);

            //turretHead.LookAt(ignoreY);

            if (!feelText.transform.GetChild(0).gameObject.activeSelf)
            { 
                AttackArea.color = Color.red;

                feelText.transform.GetChild(0).gameObject.SetActive(true);
                //feelText.transform.GetChild(1).gameObject.SetActive(false);

            }

               

            timer += Time.deltaTime;

            if (timer >= interval)
            {
                timer = 0f;
                ShootOn();
            }
           
        }
        else
        {
            if(feelText.transform.GetChild(0).gameObject.activeSelf)
            {
                timer = 0f;
                AttackArea.color = Color.green;
                feelText.transform.GetChild(0).gameObject.SetActive(false);
            }
            turretHead.Rotate(Vector3.up * 100 * Time.deltaTime);

        }


    }

    private void ShootOn()
    {
        GameObject Bullet = Instantiate(bullet, turretHead.position, turretHead.rotation);
        Destroy(Bullet, 3f);
    }

    private void moveRandom()
    {

        float distanceToPlayer = Vector3.Distance(transform.position, targetPlayer.position);
        float rotationSpeed = 5f; // 속도 조절 가능

        if (distanceToPlayer <= detectionRange)
        {
            // 플레이어 방향으로 이동
            Vector3 direction = (targetPlayer.position - transform.position).normalized;
            transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);


            Quaternion targetRotation = Quaternion.LookRotation(direction);

            turretBody.rotation = Quaternion.Slerp(turretBody.rotation, targetRotation, Time.deltaTime * rotationSpeed);

            feelText.transform.GetChild(1).gameObject.SetActive(true);
           // feelText.transform.GetChild(0).gameObject.SetActive(false);  
        }
        else
        {
            feelText.transform.GetChild(1).gameObject.SetActive(false);
            // 랜덤 방향으로 이동
            moveTimer += Time.deltaTime;
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);

            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);

           
            turretBody.rotation = Quaternion.Slerp(turretBody.rotation, targetRotation, Time.deltaTime * rotationSpeed);

            if (moveTimer >= moveChangeInterval)
            {
                int Choice = UnityEngine.Random.Range(0,2);

                switch (Choice)
                {
                    case 0:
                        PickNewMoveDirection();
                        break;

                    case 1:
                        break;

                    case 2:
                        break;
 
                    default:
                        break;
                }

                
                moveTimer = 0f;
            }
        }
    }

}
