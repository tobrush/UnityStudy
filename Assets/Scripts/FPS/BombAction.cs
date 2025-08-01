using UnityEngine;

public class BombAction : MonoBehaviour
{
    public GameObject effectPrefab;

    private Rigidbody bombRb;

    public float bombRange = 10f;
    public float boomPower = 100f;
    public LayerMask layerMask;

    void Awake()
    {
        bombRb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        GameObject BommbEffect = Instantiate(effectPrefab);
        BommbEffect.transform.position = transform.position;

        Collider[] colliders = Physics.OverlapSphere(transform.position, bombRange, layerMask);
        foreach (var collider in colliders)
        {
            var rb = collider.GetComponent<Rigidbody>();

            // AddExplosionForce(���� �Ŀ�, ���� ��ġ, ���� ����, ���� ����)
            rb.AddExplosionForce(boomPower, transform.position, bombRange, 1f);
        }



        Destroy(gameObject);
    }
}
