using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransitionFader : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 1f;
    public Color fadeColor = Color.black;

    private bool isFading = false;

    private void Start()
    {
        // Start the fade-in effect when the scene loads
        StartCoroutine(Fade(1f, 0f, () =>
        {
            gameObject.SetActive(false);
        }));
    }

    public void FadeToScene(string sceneName)
    {
        if (!isFading)
        {
            gameObject.SetActive(true);
            isFading = true;
            StartCoroutine(Fade(0f, 1f, () =>
            {
                SceneManager.LoadScene(sceneName);
            }));
        }
    }

    private IEnumerator Fade(float startAlpha, float endAlpha, System.Action onFadeComplete = null)
    {
        fadeImage.color = fadeColor;
        fadeImage.canvasRenderer.SetAlpha(startAlpha);

        fadeImage.CrossFadeAlpha(endAlpha, fadeDuration, true);

        yield return new WaitForSeconds(fadeDuration);

        if (onFadeComplete != null)
        {
            onFadeComplete.Invoke();
        }

        isFading = false;
    }
}
