using TMPro;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    public TextMeshProUGUI currentScoreUI;
    public TextMeshProUGUI bestScoreUI;

    private int currentScore;
    private int bestScore;


    private GameObject player;
    private bool wasPlayerAlive = true;

    public int Scroe
    {
        get
        {
            return currentScore;
        }
        set
        {
            currentScore = value;

            currentScoreUI.text = "���� ���� : " + currentScore;

            if (currentScore > bestScore)
            {
                bestScore = currentScore;
                bestScoreUI.text = "�ְ� ���� : " + bestScore;

                PlayerPrefs.SetInt("BestScore", bestScore);
            }
        }
    }


    void Start()
    {
 
        if (player == null)
        {
            player = GameObject.Find("Player");
            wasPlayerAlive = true;
        }


            bestScore = PlayerPrefs.GetInt("BestScore", 0);

        bestScoreUI.text = "�ְ� ���� : " + bestScore;
    }

    private void Update()
    {
        if (wasPlayerAlive && player == null)
        {
            Debug.Log("GameOver");
            wasPlayerAlive = false;
        }
    }

    /*
    public void SetScore(int value)
    {
        currentScore = value;

        currentScoreUI.text = "���� ���� : " + currentScore;

        if (currentScore > bestScore)
        {
            bestScore = currentScore;
            bestScoreUI.text = "�ְ� ���� : " + bestScore;

            PlayerPrefs.SetInt("BestScore", bestScore);
        }
    }

    public int GetScore()
    {
        return currentScore;
    }
    */
}
