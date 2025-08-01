using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeRoutine : MonoBehaviour
{
    public Image fadePanel;

    public void OnFade(float fadeTime, Color color)
    {
        StartCoroutine(Fade(fadeTime, color, true));
    }

    public IEnumerator Fade(float fadeTime, Color color, bool isFadeStart)
    {
        float timer = 0f;
        float percent = 0f;
        while (percent < 1f)
        {
            timer += Time.deltaTime;
            percent = timer / fadeTime;

            

            fadePanel.color = new Color(color.r, color.g, color.b, percent);
            yield return null;
        }
    }
}
