using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public int poolSize = 10;

    public GameObject[] enemyObjectPool;
    public Transform[] spawnPoints;

    private float currentTime; // Ÿ�̸�

    private float minTime = 1;
    private float maxTime = 5;

    public float createTime = 1f; // ���� �ֱ�

    public GameObject enemyFactory;

    void Start()
    {
        createTime = Random.Range(minTime, maxTime);

        enemyObjectPool = new GameObject[poolSize];

        for (int i = 0; i < poolSize; i++)
        {
            GameObject enemy = Instantiate(enemyFactory);

            enemyObjectPool[i] = enemy;
            enemy.SetActive(false);
        }
    }

    void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime > createTime)
        {
            for (int i = 0; i < poolSize; i++)
            {
                GameObject enemy = enemyObjectPool[i];

                if (!enemy.activeSelf)
                {
                    
                    enemy.transform.position = spawnPoints[Random.Range(0, spawnPoints.Length-1)].position; // ��ġ �ʱ�ȭ
                    enemy.SetActive(true);
                    currentTime = 0f;
                    break;
                }


            }
            
        }
    }
}