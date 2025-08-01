using UnityEngine;
using UnityEngine.Pool;

public class PoolManager : MonoBehaviour
{
    public ObjectPool<GameObject> pool;
    public GameObject prefab;

    void Awake()
    {
        pool = new ObjectPool<GameObject>(CreateObject, OnGetObject, OnReleaseObject, OnDestroyObject, maxSize: 100);
    }

    private GameObject CreateObject()
    {
        GameObject obj = Instantiate(prefab);
        obj.SetActive(false);

        return obj;
    }

    private void OnGetObject(GameObject obj)
    {
        //obj.transform.position = Vector3.zero;
        obj.SetActive(true);
    }

    private void OnReleaseObject(GameObject obj)
    {
        obj.SetActive(false);
    }

    private void OnDestroyObject(GameObject obj)
    {
        Destroy(obj);
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            GameObject obj = pool.Get();

        }
    }
}