using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class FadeController : MonoBehaviour
{
    public CanvasGroup fadeCanvas;   // the black overlay
    public float fadeDuration = 1f;  // how long fade lasts (seconds)

    private void Start()
    {
        // start transparent (for when game begins)
        if (fadeCanvas != null)
            fadeCanvas.alpha = 0f;
    }

    public void FadeAndLoadScene(string sceneName)
    {
        StartCoroutine(FadeOutAndLoad(sceneName));
    }

    private IEnumerator FadeOutAndLoad(string sceneName)
    {
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            fadeCanvas.alpha = Mathf.Lerp(0, 1, t / fadeDuration);
            yield return null;
        }

        // full black, now load the scene
        SceneManager.LoadScene(sceneName);
    }
}
