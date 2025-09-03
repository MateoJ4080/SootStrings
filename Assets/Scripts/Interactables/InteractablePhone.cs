using UnityEngine;

public class InteractablePhone : MonoBehaviour, IInteractable
{
    private bool isActive;
    public bool IsActive => isActive;

    [SerializeField] private AudioSource ringAudio;

    public void Interact(GameObject gameObject)
    {
        StopRing();
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
