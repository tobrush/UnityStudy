using System.Collections;
using UnityEngine;

public class BombRespawn : MonoBehaviour
{
    public GameObject bombPrefab;
    public int rangeX = 5;
    public int rangeZ = 5;
    public float height = 5f;

    IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            RespawnLoop();
        }
    }

    private void RespawnLoop() // 생성하는 기능
    {
        Vector3 randomPos = new Vector3(Random.Range(-rangeX, rangeX + 1), height, Random.Range(-rangeZ, rangeZ + 1));
        GameObject bomb = Instantiate(bombPrefab, randomPos, Quaternion.identity);

        Vector3 randomRot = new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)).normalized;
        bomb.GetComponent<Rigidbody>().AddTorque(randomRot);
    }
}