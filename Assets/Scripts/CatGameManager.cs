using TMPro;
using UnityEngine;

public class CatGameManager : MonoBehaviour
{
    private static CatGameManager instance = null;

    public SoundManager soundManager;

    public TextMeshProUGUI playTimeUI;
    public TextMeshProUGUI scoreUI;

    private float timer;
    public static int score; // 사과를 먹은 개수
    public static bool isPlay;

    void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public static CatGameManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }


    void Start()
    {
        soundManager.SetBGMSound("Intro");
        isPlay = true;
    }

    void Update()
    {
        if (!isPlay)
            return;

        timer += Time.deltaTime;

        playTimeUI.text = $"플레이 시간 : {timer:F1}초";
        scoreUI.text = $"X {score}";
    }
}
