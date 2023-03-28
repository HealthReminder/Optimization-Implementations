using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackAndForthMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Vector3 _displacement;
    [SerializeField] private bool _isTriggered = false;
    [SerializeField] private AnimationCurve _movementCurve;
    [SerializeField] private bool _isMoving = false;
    [ContextMenu("Move")]
    public void Move()
    {
        StartCoroutine(MoveRoutine());
    }
    IEnumerator MoveRoutine()
    {
        while (_isMoving)
            yield break;
        _isMoving = true;
        float progress = 0;
        Vector3 initialPos = transform.position;
        Vector3 targetPos;
        if (!_isTriggered)
            targetPos = initialPos + _displacement;
        else
            targetPos = initialPos - _displacement;

        while (progress < 1) {
                transform.position = Vector3.Lerp(initialPos, targetPos, _movementCurve.Evaluate(progress));
            progress += Time.deltaTime*_speed;
            yield return null;
        }
        transform.position = targetPos;
        _isTriggered = !_isTriggered;
        _isMoving = false;
        yield break;
    }
}
