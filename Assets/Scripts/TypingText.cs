using System.Collections;
using TMPro;
using UnityEngine;


public class TypingText : MonoBehaviour
{
    public TextMeshProUGUI textUI;
    private string currText;
    public float typingSpeed = 0.025f;

    public void EventStart()
    {
       // textUI = this.GetComponent<TextMeshProUGUI>();
        currText = textUI.text;
        textUI.text = string.Empty;

        StartCoroutine(TypingRoutine());
    }




    IEnumerator TypingRoutine()
    {
        int textCount = currText.Length;
        for (int i = 0; i < textCount; i++)
        {
            textUI.text += currText[i];
            yield return new WaitForSeconds(typingSpeed);
        }
    }

}