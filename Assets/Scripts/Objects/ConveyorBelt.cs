using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Moves rigidbody in a direction as a conveyor belt would
/// </summary>
public class ConveyorBelt : MonoBehaviour
{
    [SerializeField] private bool _isOn = true; // Is moving objects
    [SerializeField] private Vector3 forceDirection; // The direction objects will be moved
    [SerializeField] private float forceMultiplier; // The direction objects will be moved
    private List<Rigidbody> _restingRigidbodies; // Records the rigidbodies that are resting on the belt
    private void Awake()
    {
        _restingRigidbodies = new List<Rigidbody>();
    }
    /// <summary>
    /// Record rigidbodies that came in contact with the belt
    /// So they can be awaken later
    /// </summary>
    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = collision.collider.GetComponent<Rigidbody>();
        if (rb != null)
        {
            if (!_restingRigidbodies.Contains(rb))
                _restingRigidbodies.Add(rb);
        }
    }
    /// <summary>
    /// Record rigidbodies that lost contact with the belt
    /// </summary>
    private void OnCollisionExit(Collision collision)
    {
        Rigidbody rb = collision.collider.GetComponent<Rigidbody>();
        if (rb != null)
        {
            if (_restingRigidbodies.Contains(rb))
                _restingRigidbodies.Remove(rb);
        }
    }
    /// <summary>
    /// Move the rigidbodies in contact with the belt
    /// </summary>
    private void OnCollisionStay(Collision collision)
    {
        if (!_isOn)
            return;

        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
        if (rb)
        {
            rb.MovePosition(rb.position + forceDirection * forceMultiplier *Time.deltaTime);
            rb.angularVelocity = collision.gameObject.GetComponent<Rigidbody>().angularVelocity * 0.8f;
            //collision.gameObject.GetComponent<Rigidbody>().drag = 0;
        }
    }
    /// <summary>
    /// Toggle the belt on or off
    /// </summary>
    /// <param name="isOn">New state of the belt</param>
    public void Toggle(bool isOn)
    {
        _isOn = isOn;
        if(isOn)
            foreach (Rigidbody rb in _restingRigidbodies)
            {
                rb.AddForce(Vector3.up * 2f, ForceMode.Impulse);
            }
    }
}
