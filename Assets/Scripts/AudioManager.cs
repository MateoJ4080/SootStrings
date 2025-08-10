using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private int poolSize = 6;
    private List<AudioSource> audioSources;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            audioSources = new List<AudioSource>();
            for (int i = 0; i < poolSize; i++)
            {
                AudioSource source = gameObject.AddComponent<AudioSource>();
                audioSources.Add(source);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(AudioClip clip, float volume = 1f)
    {
        AudioSource source = GetAvailableSource();
        if (source != null)
        {
            source.clip = clip;
            source.volume = volume;
            source.Play();
        }
        else
        {
            Debug.LogWarning("No AudioSource available to play sound");
        }
    }

    private AudioSource GetAvailableSource()
    {
        foreach (var source in audioSources)
        {
            if (!source.isPlaying)
                return source;
        }
        return null;
    }
}
