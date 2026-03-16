using UnityEngine;
public class PhoneTrigger : MonoBehaviour
{
    [SerializeField] private LandlineMission _landlineMission;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        _landlineMission.OnFaintTrigger();
    }
}