using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float sensitivity = 2f;

    private Rigidbody rb;
    private float mouseX;
    private float mouseY;

    void Start()
    {
        // Get reference to Rigidbody component
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Freeze rotation to prevent unwanted movement
    }

    void Update()
    {
        // Get player input for movement and look
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");
        mouseX += Input.GetAxisRaw("Mouse X") * sensitivity;
        mouseY -= Input.GetAxisRaw("Mouse Y") * sensitivity;

        // Limit vertical look angle
        mouseY = Mathf.Clamp(mouseY, -90f, 90f);

        // Rotate player based on mouse movement
        transform.eulerAngles = new Vector3(mouseY, mouseX, 0f);

        // Calculate movement direction based on input and current rotation
        Vector3 moveHorizontal = transform.right * moveX;
        Vector3 moveVertical = transform.forward * moveZ;
        Vector3 moveDir = (moveHorizontal + moveVertical).normalized;

        // Move player based on calculated direction and speed using Rigidbody
        rb.MovePosition(transform.position + moveDir * speed * Time.fixedDeltaTime);
    }
}