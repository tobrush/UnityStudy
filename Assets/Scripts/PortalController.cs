using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PortalController : MonoBehaviour
{
    public GameObject portalImpact;
    public FadeRoutine fade;
    public GameObject loadingImage;
    public Image progressBar;

    private Coroutine runningCoroutine;

    void OnTriggerEnter2D(Collider2D other)
    {
        runningCoroutine = StartCoroutine(PortalRoutine());
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        portalImpact.SetActive(false);
        StopCoroutine(runningCoroutine);
    }

    IEnumerator PortalRoutine()
    {
        portalImpact.SetActive(true);
        yield return new WaitForSeconds(2f);

        yield return StartCoroutine(fade.Fade(3f, Color.white, true));
        loadingImage.SetActive(true);

        yield return StartCoroutine(fade.Fade(3f, Color.white, false));

        while (progressBar.fillAmount < 1f)
        {
            var ranValue = Random.Range(0.0001f, 0.001f);
            progressBar.fillAmount += ranValue;
            yield return null;
        }

        SceneManager.LoadScene(1);
    }
}