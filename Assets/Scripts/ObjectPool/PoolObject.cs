using UnityEngine;

public class PoolObject : MonoBehaviour
{
    private ObjectPoolQueue pool;

    void Awake()
    {
        pool = FindFirstObjectByType<ObjectPoolQueue>();
    }

    void OnEnable()
    {
        Invoke("ReturnPool", 3f);
    }

    private void ReturnPool()
    {
        pool.EnQueueObject(gameObject);
    }
}