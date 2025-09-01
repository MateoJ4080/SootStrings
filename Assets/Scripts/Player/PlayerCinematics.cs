using System.Collections;
using UnityEngine;

public class PlayerCinematics : MonoBehaviour
{
    public IEnumerator SmoothMoveTo(Vector3 targetPos, float smoothTime)
    {
        Vector3 velocity = Vector3.zero;

        while (Vector3.Distance(transform.position, targetPos) > 0.01f)
        {
            transform.position = Vector3.SmoothDamp(
                transform.position,
                targetPos,
                ref velocity,
                smoothTime
            );

            yield return null;
        }

        transform.position = targetPos;
    }
}