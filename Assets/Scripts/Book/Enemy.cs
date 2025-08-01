using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    private Vector3 dir;
    public float speed = 5;

    public GameObject explosionFactory;

    void Start()
    {
        int ranValue = UnityEngine.Random.Range(0, 10);

        if (ranValue < 3) // 30%
        {
            GameObject target = GameObject.Find("Player");
            if (target != null)
            {
                dir = target.transform.position - transform.position; // 플레이어를 바라보는 방향 값
                dir.Normalize();
            }
                
        }
        else // 70%
        {
            dir = Vector3.back;
        }
    }

    void Update()
    {
        transform.position += dir * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision other)
    {
        // 점수 증가
        //GameObject smObject = GameObject.Find("ScoreManager");
        //ScoreManager sm = smObject.GetComponent<ScoreManager>();

        // sm.SetScore(sm.GetScore() + 1); // 책에 적힌 거
        //var score = sm.GetScore() + 1;
        //sm.SetScore(score);
        ScoreManager.Instance.Scroe++;

        // 파티클 생성
        GameObject explosion = Instantiate(explosionFactory);
        explosion.transform.position = transform.position;

        // 파괴 기능
      //  Destroy(other.gameObject);
        if(other.gameObject.name.Contains("Bullet"))
        {
            other.gameObject.SetActive(false);
        }
        else
        {
              Destroy(other.gameObject);
        }

        //Destroy(gameObject);
        this.gameObject.SetActive(false);
    }
}
