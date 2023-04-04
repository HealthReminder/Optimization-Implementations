using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtSmooth : MonoBehaviour
{
    [SerializeField] private AnimationCurve _movementCurve;
    private Quaternion _targetRotation;

    float progress;
    public IEnumerator LookAtRoutine(Transform target, float speed = 1)
    {
        progress = 0;
        // Get the direction to the target
        Vector3 targetDirection = target.position - transform.position;
        // Calculate the rotation required to look at the target
        _targetRotation = Quaternion.LookRotation(targetDirection);
        while (progress < 1)
        {
            // Gradually turn towards the target rotation
            transform.rotation = Quaternion.Lerp(transform.rotation, _targetRotation, _movementCurve.Evaluate(progress));
            progress += Time.deltaTime * speed;
            yield return null;
        }
        yield return null;
        transform.rotation = _targetRotation;


        yield break;
    }
}
