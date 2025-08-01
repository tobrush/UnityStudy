using UnityEngine;

public class Pinball : MonoBehaviour
{
    public PinballManager manager;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Scroe10"))
        {
            manager.TotalScore += 10;
        }
        if (collision.gameObject.CompareTag("Scroe30"))
        {
            manager.TotalScore += 30;
        }
        if (collision.gameObject.CompareTag("Scroe50"))
        {
            manager.TotalScore += 50;
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("GameOver"))
        {
            Debug.Log("Gameover");
        }
    }
}
