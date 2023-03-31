using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// Triggers events once after some time has elapsed
/// </summary>
public class DelayedEvent : MonoBehaviour
{
    [SerializeField] float _timeToTrigger = 1.0f;
    [SerializeField] UnityEvent _events;
    private float _elapsedTime = 0.0f;
    private bool _isTriggered = false;
    private void Update()
    {
        if (_isTriggered)
        {
            Destroy(this);
            return;
        }

        _elapsedTime += Time.deltaTime;
        if (_elapsedTime >= _timeToTrigger)
        {
            _events.Invoke();
            _isTriggered = true;
        }
    }
}
