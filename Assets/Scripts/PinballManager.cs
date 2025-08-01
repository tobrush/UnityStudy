using TMPro;
using UnityEngine;

public class PinballManager : MonoBehaviour
{
    public Rigidbody2D leftStickRb;
    public Rigidbody2D rightStickRb;

    public int TotalScore = 0;


    public TMP_Text scoreText;

    void Update()
    {
        scoreText.text = TotalScore.ToString();


        if (Input.GetKey(KeyCode.LeftArrow))
            leftStickRb.AddTorque(30f);
        else
            leftStickRb.AddTorque(-25f);

        if (Input.GetKey(KeyCode.RightArrow))
            rightStickRb.AddTorque(-30f);
        else
            rightStickRb.AddTorque(25f);
    }
}