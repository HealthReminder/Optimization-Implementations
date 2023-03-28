using UnityEngine;
using UnityEngine.Events;
using System.Collections;

/// <summary>
/// This object will measure the rotation of a lever handle 
/// And invoke events based on its state
/// </summary>
public class Lever : MonoBehaviour
{
    [SerializeField] private Transform _leverHandle;
    [SerializeField] private Rigidbody _leverRb;
    [SerializeField] private bool _currentState;
    [SerializeField] private bool _lastState;
    private float _currentAngle;
    [SerializeField] UnityEvent[] OnMaxEvents; // Events triggered when the handle is > 45 degrees
    [SerializeField] UnityEvent[] OnMinEvents; // Events triggered when the handle is < -45 degrees

    /// <summary>
    /// Record the state of the lever based on its angle
    /// </summary>
    void Update()
    {
        _currentAngle = _leverHandle.localEulerAngles.x; // Get current angle of the handle
        if (_currentAngle > 180)
            _currentAngle -= 360;

        // Check if the handle is at the minimum or maximum angle
        if (_currentAngle >= 30)
        {
            _currentState = true;
            if (_currentState != _lastState)
            {
                InvokeEvents(OnMaxEvents);
                StartCoroutine(JamRoutine());
            }
        }
        else if (_currentAngle <= -30){
            _currentState = false;
            if (_currentState != _lastState)
            {
                InvokeEvents(OnMinEvents);
                StartCoroutine(JamRoutine());
            }
        }
        _lastState = _currentState;
    }
    IEnumerator JamRoutine()
    {
        _leverRb.isKinematic = true;
        _leverRb.velocity = Vector3.zero;
        yield return new WaitForSeconds(2);
        _leverRb.isKinematic = false;
        yield break;
    }
    /// <summary>
    /// Perform action when handle is at maximum angle
    /// </summary>
    void InvokeEvents(UnityEvent[] events)
    {
        foreach (UnityEvent e in events)
            e.Invoke();
    }
}