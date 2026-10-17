using System.Collections;
using UnityEngine;

public class InteractableBed : Interactable
{
    [SerializeField] private InteractableCellphone _interactableCellphone;

    public override void Interact(GameObject gameObject)
    {
        StartCoroutine(Execute());
    }

    private IEnumerator Execute()
    {
        Deactivate();

        yield return FadeManager.Instance.FadeIn();
        yield return FadeManager.Instance.FadeOut();

        _interactableCellphone.Activate();
    }
}