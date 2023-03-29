using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// This script simulates an object that moves smoothly, according to a curve,
/// Back and forth from its original position to a new position  
/// </summary>
public class BackAndForthMovement : MonoBehaviour
{
    [SerializeField] private UnityEvent OnMoveAway;
    [SerializeField] private UnityEvent OnMoveBack;
    [SerializeField] private float _speed;  // How fast the object moves    
    [SerializeField] private Vector3 _displacement; // Displacement from original position
    [SerializeField] private AnimationCurve _movementCurve; // The curve that dictates how the object moves
    [SerializeField] private bool _isMoving = false; // Records if the movement is currently happening
    [SerializeField] private bool _isRetracted = false; // If the arm is on the lower position
    private float _movementInterpolation = 0; // How much it has moved (0-1)

    public bool IsRetracted { get { return _isRetracted; } }  // Is Retracted variable public and readonly
    public bool IsMoving { get { return _isMoving; } } // Is Moving variable public and readonly
    /// <summary>
    /// Toggle the object movement
    /// </summary>
    [ContextMenu("Move")]
    public void Move()
    {
        StartCoroutine(MoveRoutine());
    }

    /// <summary>
    /// Perform the smooth movement
    /// </summary>
    IEnumerator MoveRoutine()
    {
        while (_isMoving)
            yield break;

        _isMoving = true;
        _movementInterpolation = 0;
        Vector3 initialPos = transform.position;
        Vector3 targetPos;

        // Figure out which direction it is going
        if (!_isRetracted)
            targetPos = initialPos + _displacement;
        else
            targetPos = initialPos - _displacement;

        // Move it to the new position
        while (_movementInterpolation < 1)
        {
            transform.position = Vector3.Lerp(initialPos, targetPos, _movementCurve.Evaluate(_movementInterpolation));
            _movementInterpolation += Time.deltaTime * _speed;
            yield return null;
        }
        transform.position = targetPos;
        _isRetracted = !_isRetracted;

        // Trigger position events
        if (!IsRetracted)
            OnMoveBack.Invoke();
        else
            OnMoveAway.Invoke();


        _isMoving = false;
        yield break;
    }
}
