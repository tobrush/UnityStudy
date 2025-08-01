using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class Bomb : MonoBehaviour
{
    private Rigidbody bombRb;
    public GameObject ps;
    public LayerMask layerMask;
    public float bombTimer = 4f;
    public float bombRange = 10f;
    public float boomPower = 100f;

    void Awake()
    {
        bombRb = GetComponent<Rigidbody>();
    }

    IEnumerator Start()
    {
       
        yield return new WaitForSeconds(bombTimer);
        ps.SetActive(true);
        BombForce();
    }

    public void BombForce()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, bombRange, layerMask);
        foreach (var collider in colliders)
        {
            var rb = collider.GetComponent<Rigidbody>();

            // AddExplosionForce(Æø¹ß ÆÄ¿ö, Æø¹ß À§Ä¡, Æø¹ß ¹üÀ§, Æø¹ß ³ôÀÌ)
            rb.AddExplosionForce(boomPower, transform.position, bombRange, 1f);
        }

        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(3).gameObject.SetActive(true);
        transform.GetChild(4).gameObject.SetActive(true);

        //Destroy(gameObject);

        StartCoroutine(DestroyThis());
    }

    IEnumerator DestroyThis()
    {
        Color color = transform.GetChild(3).gameObject.GetComponent<SpriteRenderer>().color;
        float alpha = color.a;
        float elapsed = 0f;

        while (alpha > 0f)
        {
            elapsed += Time.deltaTime;
            alpha = Mathf.Lerp(1f, 0f, elapsed / bombTimer);
            color.a = alpha;
            transform.GetChild(3).gameObject.GetComponent<SpriteRenderer>().color = color;
            transform.GetChild(4).gameObject.GetComponent<SpriteRenderer>().color = color;
            yield return null;
        }

        yield return new WaitForSeconds(bombTimer);
        Destroy(gameObject);
    }

   
}