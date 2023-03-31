using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class DisableKinemactics : MonoBehaviour
{
    private Rigidbody _rb;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    public void Disable ()
    {
        _rb.velocity = Vector3.zero;
        _rb.isKinematic = false;
    }
}
