using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float bulletSpeed = 10f;

    void Update()
    {
        transform.position += transform.forward * bulletSpeed * Time.deltaTime;
       // transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
    }
}
