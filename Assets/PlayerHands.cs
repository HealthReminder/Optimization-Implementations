using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class handles everything related to moving objects in the world
/// </summary>
public class PlayerHands : MonoBehaviour
{
    [SerializeField] private float _forceMultiplier = 0.1f; ///Multiplies the force applied to move the object being held
    [SerializeField] private Transform _anchorPoint; /// The position the held object will move to
    [SerializeField] private Camera _playerCamera;  /// The player camera from where rays will be shot
    private Rigidbody _currentlyHolding;    /// The rigidbody the player is currently holding
    private float _dampingDistance = 0.2f; /// Distance from the anchor point where damping will start
    [SerializeField] private float _springStrength = 50f;
    [SerializeField] private float _springDamping = 0.7f;

    [SerializeField] private SpringJoint _springJoint;

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            _currentlyHolding = null;
        }
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _playerCamera.ViewportPointToRay(new Vector2(0.5f, 0.5f));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                if (hit.collider.attachedRigidbody)
                {
                    if (!hit.collider.attachedRigidbody.isKinematic)
                    {
                        _currentlyHolding = hit.collider.attachedRigidbody;
                        MoveRigidbody();
                        Debug.DrawLine(ray.origin, hit.point, Color.magenta, 2);
                    }
                }
            }
        }
        //if (Input.GetMouseButton(0))
            //MoveRigidbody();
    }
    public void MoveRigidbody()
    {
        if (_currentlyHolding != null && _anchorPoint != null)
        {
            // Get the direction from the current position to the anchor point
            Vector3 direction = _anchorPoint.position - _currentlyHolding.transform.position;

            // Set the connected anchor of the SpringJoint to be the direction vector
            _springJoint.connectedAnchor = direction/2;

            // Set the anchor position of the SpringJoint to be the current position of the object
            _springJoint.anchor = Vector3.zero;

            // Set the connected body of the SpringJoint to be the Rigidbody of the object
            _springJoint.connectedBody = _currentlyHolding;

            // Adjust the SpringJoint's strength and damper values to control the behavior of the spring
            _springJoint.spring = _springStrength;
            _springJoint.damper = _springDamping;
        }
    }
}
