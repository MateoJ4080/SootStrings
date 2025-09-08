using UnityEngine;
using System.Collections;

public class InteractableLandlinePhone : MonoBehaviour, IInteractable
{
    private bool isActive;
    public bool IsActive
    {
        get => isActive;
        set => isActive = value;
    }

    [SerializeField] private AudioSource ringAudio;

    public void Interact(GameObject gameObject)
    {
        StopRing();
    }

    public IEnumerator Ring()
    {
        if (!ringAudio.isPlaying)
        {
            isActive = true;
            ringAudio.loop = true;
            ringAudio.Play();
        }
        yield break;
    }

    public void StopRing()
    {
        isActive = false;
        ringAudio.Stop();
    }
}
