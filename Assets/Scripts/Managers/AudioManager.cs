using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public IEnumerator PlayMusicCoroutine(AudioClip clip, float volume = 1f)
    {
        musicSource.clip = clip;
        musicSource.volume = volume * 0.1f;
        musicSource.Play();
        yield break;
    }

    public IEnumerator PlaySFXCoroutine(AudioClip clip, float volume = 1f)
    {
        sfxSource.PlayOneShot(clip, volume * 0.1f);
        yield break;
    }
}
