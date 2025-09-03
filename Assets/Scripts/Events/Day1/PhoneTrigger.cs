using UnityEngine;

public class PhoneTrigger : MonoBehaviour
{
    [SerializeField] private PhoneEvent phoneEvent;
    [SerializeField] private bool isFaintTrigger = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (isFaintTrigger)
            phoneEvent.OnFaintTrigger();
        else
            phoneEvent.OnStartEffectsTrigger();
    }
}