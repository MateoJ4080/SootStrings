using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class InteractDoor : MonoBehaviour, IInteractable
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float _speed = 1f;

    private bool _isOpen = false;
    private Coroutine _doorRoutine;


    public void Interact(GameObject gameObject)
    {
        _isOpen = !_isOpen;

        if (_doorRoutine != null)
            StopCoroutine(_doorRoutine);

        // Target value for the Blend Tree: 1 = fully open, 0 = fully closed
        float target = _isOpen ? 1f : 0f;
        _doorRoutine = StartCoroutine(AnimateDoor(target));
    }

    // Using coroutine to avoid doing it on the Update
    private IEnumerator AnimateDoor(float target)
    {

        float current = _animator.GetFloat("DoorProgress");

        while (!Mathf.Approximately(current, target))
        {
            current = Mathf.MoveTowards(current, target, Time.deltaTime * _speed);
            _animator.SetFloat("DoorProgress", current);
            yield return null;
        }
    }
}
