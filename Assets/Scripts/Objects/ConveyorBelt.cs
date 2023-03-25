using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    [SerializeField] private bool _isOn = true;
    [SerializeField] private Vector3 forceDirection; // The direction objects will be moved
    [SerializeField] private float forceMultiplier; // The direction objects will be moved
    private List<Rigidbody> _restingRigidbodies; // Records the rigidbodies that are resting on the belt
    private void Awake()
    {
        _restingRigidbodies = new List<Rigidbody>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = collision.collider.GetComponent<Rigidbody>();
        if (rb != null)
        {
            if (!_restingRigidbodies.Contains(rb))
                _restingRigidbodies.Add(rb);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        Rigidbody rb = collision.collider.GetComponent<Rigidbody>();
        if (rb != null)
        {
            if (_restingRigidbodies.Contains(rb))
                _restingRigidbodies.Remove(rb);
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (!_isOn)
            return;

        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
        if (rb)
        {
            rb.MovePosition(rb.position + forceDirection * forceMultiplier *Time.deltaTime);
            rb.angularVelocity = collision.gameObject.GetComponent<Rigidbody>().angularVelocity * 0.7f;
            //collision.gameObject.GetComponent<Rigidbody>().drag = 0;
        }
    }
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
