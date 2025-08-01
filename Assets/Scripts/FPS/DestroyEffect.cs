using UnityEngine;

public class DestroyEffect : MonoBehaviour
{
    public float lifeTime = 6.0f;

    float currentTime = 0f;

    private void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime > lifeTime)
        {
            Destroy(gameObject);
        }

    }
}
