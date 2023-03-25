using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    [SerializeField] private bool _isOn = true;
    [SerializeField] private Vector3 forceDirection; //The direction objects will be moved
    [SerializeField] private float forceMultiplier; //The direction objects will be moved
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
    }
}
