using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    public Camera Camera;
    public GameObject targetObject; // the object to move around
    public LayerMask hitLayers; // the layers that the ray can hit
    public float maxDistance = 100f; // the maximum distance for the raycast
    public float offset = 0.1f; // offset from the surface

    private void Start()
    {
        // hide the mouse cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // cast a ray from the center of the screen to the world
        Ray ray = Camera.ViewportPointToRay(new Vector2(0.5f, 0.5f));
        RaycastHit hit;
        bool isHit = Physics.Raycast(ray, out hit, maxDistance, hitLayers);

        if (isHit)
        {
            // activate the target object and move it to the hit point
            targetObject.SetActive(true);
            targetObject.transform.position = hit.point + hit.normal * offset;

            // invert the direction of the target object and align it with the surface normal
            targetObject.transform.rotation = Quaternion.LookRotation(-hit.normal, Camera.transform.forward);
        }
        else
        {
            // deactivate the target object
            targetObject.SetActive(false);
        }
    }
}