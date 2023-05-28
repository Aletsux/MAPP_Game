using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransitionFader : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDurationIn;
    public float fadeDurationOut;
    public Color fadeColor = Color.black;

    private bool isFading = false;

    private void Start()
    {
        gameObject.SetActive(true);
        
        StartCoroutine(Fade(1f, 0f, fadeDurationIn, () =>
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
            StartCoroutine(Fade(0f, 1f, fadeDurationOut, () =>
            {
                SceneManager.LoadScene(sceneName);
            }));
        }
    }

    private IEnumerator Fade(float startAlpha, float endAlpha, float duration, System.Action onFadeComplete = null)
    {
        fadeImage.color = fadeColor;
        fadeImage.canvasRenderer.SetAlpha(startAlpha);

        fadeImage.CrossFadeAlpha(endAlpha, duration, true);

        yield return new WaitForSeconds(duration);

        if (onFadeComplete != null)
        {
            onFadeComplete.Invoke();
        }
        isFading = false;
    }
}
