using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed = 5;

    void Update()
    {
        Vector3 dir = Vector3.up;

        transform.position += dir * speed * Time.deltaTime;
    }
}

