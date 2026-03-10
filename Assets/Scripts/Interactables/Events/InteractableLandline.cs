using UnityEngine;
using System.Collections;

public class InteractableLandline : MonoBehaviour, IInteractable
{
    [SerializeField] private readonly float _interactionRange = 2f;
    public float InteractionRange => _interactionRange;

    private bool isActive;
    public bool IsActive { get => isActive; }

    [SerializeField] private AudioSource ringAudio;

    public void Interact(GameObject gameObject)
    {
        StopRing();
    }

    public void Activate()
    {
        isActive = true;
    }

    public void Deactivate()
    {
        isActive = false;
    }

    public void Ring()
    {
        if (!ringAudio.isPlaying)
        {
            isActive = true;
            ringAudio.loop = true;
            ringAudio.Play();
        }
    }

    public void StopRing()
    {
        isActive = false;
        ringAudio.Stop();
    }
}
