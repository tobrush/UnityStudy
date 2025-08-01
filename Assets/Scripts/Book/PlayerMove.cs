using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public GameObject bulletFactory;
    public GameObject firePosition;

    public int poolSize = 10;
    //public GameObject[] bulletObjectPool;  //�迭
    //public List<GameObject> bulletObjectPool; //����Ʈ
    public Queue<GameObject> bulletObjectPool; //ť



    public float speed = 5;
    public float minX = -10f;
    public float maxX = 10f;
    public float minZ = -5f;
    public float maxZ = 5f;

    void Start()
    {
        // bulletObjectPool = new GameObject[poolSize];
        // bulletObjectPool = new List<GameObject>();
        bulletObjectPool = new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletFactory);
            //bulletObjectPool[i] = bullet;
            //bulletObjectPool.Add(bullet);
            bulletObjectPool.Enqueue(bullet);
            bullet.SetActive(false);
        }
       // bulletFactory = Resources.Load<GameObject>("Bullet"); // ���ҽ� �������� �Ѿ� ������ �ε�
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 dir = new Vector3(h, 0, v);

        Vector3 newPos = transform.position + dir * speed * Time.deltaTime;

        // newPos.x = Mathf.Clamp(newPos.x, minX, maxX);
        // newPos.z = Mathf.Clamp(newPos.z, minZ, maxZ);
        // transform.position = newPos;

        Vector3 viewPos = Camera.main.WorldToViewportPoint(newPos);
        viewPos.x = Mathf.Clamp(viewPos.x, 0f, 1f);
        viewPos.y = Mathf.Clamp(viewPos.y, 0f, 1f); // z���� �ƴ϶� y�� (ī�޶� ������ �������� ���)
        newPos = Camera.main.ViewportToWorldPoint(viewPos);


        transform.position = newPos;
        //transform.position += dir * speed * Time.deltaTime;



        if (Input.GetButtonDown("Fire1"))
        {
            if (bulletObjectPool.Count > 0)
            {
                GameObject bullet = bulletObjectPool.Dequeue();
                bullet.SetActive(true);
                bullet.transform.position = firePosition.transform.position;
            }

            /*
            for (int i = 0; i < poolSize; i++)
            {
               // GameObject bullet = bulletObjectPool[i];

                if(!bullet.activeSelf)
                {
                    bullet.transform.position = firePosition.transform.position; // ��ġ �ʱ�ȭ
                    bullet.SetActive(true);

                    break;
                }
            }
            */
            // GameObject bullet = Instantiate(bulletFactory);
           
            // bullet.transform.rotation = firePosition.transform.rotation; // ȸ�� �ʱ�ȭ

            // bullet.transform.SetPositionAndRotation(��ġ, ȸ��); // ��ġ�� ȸ���� �ѹ��� �����ϴ� ���
            // bullet.transform.SetParent(�θ�); // �θ� ������Ʈ ����
        }
    }


}
