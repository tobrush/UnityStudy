using UnityEngine;

public class PoolController : MonoBehaviour
{
    public ObjectPoolQueue pool;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            pool.DequeueObject(Vector3.zero);
        }
    }
}