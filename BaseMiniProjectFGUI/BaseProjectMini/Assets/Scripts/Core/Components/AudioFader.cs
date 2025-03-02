using UnityEngine;

public class AudioFader : MonoBehaviour
{
    public AudioSource audioSource;
    public float fadeDuration = 2.0f; // 淡入淡出的持续时间

    private float targetVolume;
    private float initialVolume;
    private float currentFadeTime;

    private bool isFading = false;
    private bool isFadeIn = false;
    private bool isFadeOut = false;

    void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        initialVolume = audioSource.volume;
    }

    void Update()
    {
        if (isFading)
        {
            currentFadeTime += Time.deltaTime;

            if (currentFadeTime > fadeDuration)
            {
                currentFadeTime = fadeDuration;
                isFading = false;
            }

            float t = currentFadeTime / fadeDuration;
            audioSource.volume = Mathf.Lerp(initialVolume, targetVolume, t);
        }
        else
        {
            if (isFadeIn && audioSource.time < fadeDuration / 2)
            {
                FadeIn();
            }
            if (isFadeOut && audioSource.time + fadeDuration >= audioSource.clip.length)
            {
                FadeOut();
            }
        }
    }

    public void SetFade(bool isFadeIn, bool isFadeOut)
    {
        this.isFadeIn = isFadeIn;
        this.isFadeOut = isFadeOut;
    }

    public bool IsFadeIn { get { return isFadeIn; } }
    public bool IsFadeOut { get {  return isFadeOut; } }

    public void FadeIn()
    {
        initialVolume = 0f;
        targetVolume = 1f;
        currentFadeTime = 0f;
        isFading = true;
    }

    public void FadeOut()
    {
        initialVolume = audioSource.volume;
        targetVolume = 0f;
        currentFadeTime = 0f;
        isFading = true;
    }
}