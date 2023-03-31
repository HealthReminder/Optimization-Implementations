using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CustomEvents;

public class OnCollisionEvent : MonoBehaviour
{
    public float minMagnitude;
    public Vector3Event[] PositionEvents;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude >= minMagnitude)
            foreach (Vector3Event e in PositionEvents)
            {
                e.Invoke(collision.contacts[0].point);
            }
    }
}
