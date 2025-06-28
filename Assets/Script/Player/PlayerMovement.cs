using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
<<<<<<< HEAD
    public FloatingJoystick joystick;
=======
    public FixedJoystick joystick;
>>>>>>> 2c9431f406680a1057d899ea34f985fc65f63359

    private Rigidbody rb;
    private PlayerHealth playerHealth;

    private Vector3 moveDir;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerHealth = GetComponent<PlayerHealth>();
    }

    void Update()
    {
        if (playerHealth != null && playerHealth.isDead) return;

        moveDir = new Vector3(joystick.Horizontal, 0f, joystick.Vertical);
        if (moveDir != Vector3.zero)
            transform.forward = moveDir;
    }

    void FixedUpdate()
    {
        if (playerHealth != null && playerHealth.isDead) return;

        rb.MovePosition(transform.position + moveDir * moveSpeed * Time.fixedDeltaTime);
    }
}