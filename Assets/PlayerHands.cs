using UnityEngine;

/// <summary>
/// This class handles everything related to moving objects in the world
/// </summary>
public class PlayerHands : MonoBehaviour
{
    [SerializeField] private float _forceMultiplier = 0.1f; ///Multiplies the force applied to move the object being held
    [SerializeField] private Transform _anchorPoint; /// The position the held object will move to
    [SerializeField] private Camera _playerCamera;  /// The player camera from where rays will be shot
    private Rigidbody _currentlyHolding;    /// The rigidbody being held by the player
    [SerializeField] private float _springStrength = 50f; /// How hard the rigidbody will be pulled towards the anchor
    [SerializeField] private float _springDamping = 0.7f; /// How much damping applied to the rigidbody

    [SerializeField] private SpringJoint _springJoint; /// The spring used to hold objects

    private void Update()
    {
        // Let go of the rigidbody if release mouse button
        if (Input.GetMouseButtonUp(0))
        {
            _currentlyHolding = null;
            _springJoint.connectedBody = null;
        }
        // Grab a rigidbody if pressed the mouse button down
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _playerCamera.ViewportPointToRay(new Vector2(0.5f, 0.5f));
            RaycastHit hit;
            //Find a proper item to hold
            if (Physics.Raycast(ray, out hit, 2))
            {
                if (hit.collider.attachedRigidbody)
                {
                    if (!hit.collider.attachedRigidbody.isKinematic)
                    {
                        _currentlyHolding = hit.collider.attachedRigidbody;
                        GrabRigidbody();
                        Debug.DrawLine(ray.origin, hit.point, Color.magenta, 2);
                    }
                }
            }
        }
        // Apply forces while holding the rigidbody to estabilize it
        if (Input.GetMouseButton(0))
            HoldRigidbody();
    }
    /// <summary>
    /// Configure anchor to hold a new rigidbody
    /// </summary>
    public void GrabRigidbody()
    {
        if (_currentlyHolding != null)
        {
            // Get the direction from the current position to the anchor point
            Vector3 direction = _anchorPoint.position - _currentlyHolding.transform.position;

            // Set the connected anchor of the SpringJoint to be the direction vector
            _springJoint.connectedAnchor = direction / 2;

            // Set the anchor position of the SpringJoint to be the current position of the object
            _springJoint.anchor = Vector3.zero;

            // Set the connected body of the SpringJoint to be the Rigidbody of the object
            _springJoint.connectedBody = _currentlyHolding;

            // Adjust the SpringJoint's strength and damper values to control the behavior of the spring
            _springJoint.spring = _springStrength;
            _springJoint.damper = _springDamping;
        }
    }
    /// <summary>
    /// Apply forces to dampen the moved oscillations caused by the spring
    /// </summary>
    public void HoldRigidbody()
    {
        if (_currentlyHolding != null)
        {
            float dist = Vector3.Distance(_currentlyHolding.transform.position, _anchorPoint.position);
            /// Do not apply any forces if the rigidbody is already close enough
            if (dist >= 0.3f) { 
                _currentlyHolding.AddForce(-1 * _currentlyHolding.velocity * 0.2f);
                _currentlyHolding.AddTorque(-1 * _currentlyHolding.angularVelocity * 0.2f);
            }
        }
    }
}
