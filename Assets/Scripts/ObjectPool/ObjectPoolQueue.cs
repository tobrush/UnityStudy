using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolQueue : MonoBehaviour
{
    private Queue<GameObject> objQueue = new Queue<GameObject>();

    [SerializeField] private GameObject objPrefab;
    [SerializeField] private Transform parent;

    void Start()
    {
        for (int i = 0; i < 100; i++)
            CreateObj();
    }

    private void CreateObj()
    {
        var obj = Instantiate(objPrefab, parent);
        obj.transform.SetParent(parent);

        EnQueueObject(obj);
    }

    public void EnQueueObject(GameObject newObj)
    {
        objQueue.Enqueue(newObj);
        newObj.transform.SetParent(parent);
        newObj.SetActive(false);
    }

    public GameObject DequeueObject(Vector3 respawnPos)
    {
        if (objQueue.Count <= 5)
            CreateObj();

        var obj = objQueue.Dequeue();
        obj.transform.position = respawnPos;
        obj.SetActive(true);

        return obj;
    }
}