using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : Singleton<LoadSceneManager>
{
    private int sceneIndex = 0;
    public int characterIndex = 0;

    protected override void Awake()
    {
        if (instance == null)
        {
            instance = this as LoadSceneManager;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    // 0 : ��Ʈ��, 1: ĳ���� ����, 2: ����
    public void OnLoadScene()
    {
        sceneIndex++;

        Fade.onFadeAction(3f, Color.white, true, () => SceneManager.LoadScene(sceneIndex));
    }

    public void SetCharacterIndex(int index)
    {
        characterIndex = index;
    }
}