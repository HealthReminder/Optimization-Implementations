using UnityEngine;

public class RotateOnAxis : MonoBehaviour
{
    public Vector3 rotationAxis = Vector3.up;  // Axis to rotate the object around
    public float rotationSpeed = 5f;           // Rotation speed in degrees per second

    void Update()
    {
        // Calculate rotation angle based on rotation speed and time
        float rotationAngle = rotationSpeed * Time.deltaTime;

        // Rotate object around specified axis
        transform.Rotate(rotationAxis.normalized * rotationAngle);
    }
}