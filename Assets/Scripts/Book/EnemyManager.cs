using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public int poolSize = 10;

    public GameObject[] enemyObjectPool;
    public Transform[] spawnPoints;

    private float currentTime; // 타이머

    private float minTime = 1;
    private float maxTime = 5;

    public float createTime = 1f; // 생성 주기

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
                    
                    enemy.transform.position = spawnPoints[Random.Range(0, spawnPoints.Length-1)].position; // 위치 초기화
                    enemy.SetActive(true);
                    currentTime = 0f;
                    break;
                }


            }
            
        }
    }
}