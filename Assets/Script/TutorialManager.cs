using System.Collections;
using UnityEngine;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    public GameObject tutorialPanel;
    public TextMeshProUGUI tutorialText;
    public float displayTime = 5f;
    public float fadeDuration = 1f;
    public float delayBeforeTutorial = 5f; // Delay sebelum tutorial muncul

    private bool tutorialActive = false; // Ubah menjadi false agar tidak langsung aktif
    private float timer;
    private CanvasGroup canvasGroup;

    void Start()
    {
        canvasGroup = tutorialPanel.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = tutorialPanel.AddComponent<CanvasGroup>();
        }
        canvasGroup.alpha = 0;

        // Mulai Coroutine untuk menampilkan tutorial setelah delay
        StartCoroutine(ShowTutorialAfterDelay());
    }

    void Update()
    {
        if (tutorialActive)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                HideTutorial();
            }
        }
    }

    IEnumerator ShowTutorialAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeTutorial);

        // Set tutorial aktif dan mulai fade-in
        tutorialActive = true;
        tutorialPanel.SetActive(true);
        timer = displayTime;
        StartCoroutine(FadeIn());
    }

    public void HideTutorial()
    {
        tutorialPanel.SetActive(false);
        tutorialActive = false;
    }

    private IEnumerator FadeIn()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = 1;
    }
}
