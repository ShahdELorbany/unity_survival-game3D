using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    private static BackgroundMusic instance;
    private static AudioSource audioSource;

    [Header("Audio Settings")]
    public AudioClip backgroundClip;
    public float volume = 0.4f;

    void Awake()
    {
        // Keep only one music player alive
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            // Create AudioSource dynamically
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = backgroundClip;
            audioSource.loop = true;
            audioSource.volume = volume;
            audioSource.playOnAwake = false;

            // Play automatically
            audioSource.Play();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void StopMusic()
    {
        if (audioSource != null)
            audioSource.Stop();
    }

    public static void PlayMusic()
    {
        if (audioSource != null && !audioSource.isPlaying)
            audioSource.Play();
    }
}
