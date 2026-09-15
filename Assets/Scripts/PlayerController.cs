using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public InputAction playerInput;
    private Vector2 controllerInput;
    public Rigidbody rb;
    public int health = 1;
    [SerializeField] float jumpForce;
    private int currentLane = 0;
    private bool grounded = true;
    private Animation animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerInput.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        controllerInput = playerInput.ReadValue<Vector2>();
        if (controllerInput.x > 0 && currentLane < 4)
        {
            DashRight();
        }
        else if (controllerInput.x < 0 && currentLane > -4)
        {
            DashLeft();
        }
    }

    private void Jump()
    {
        if (grounded == true)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            grounded = false;
        } 
    }

    private void DashLeft()
    {
        currentLane--;
    }

    private void DashRight()
    {
        currentLane++;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            GameManager.GameOver();
        }
        
    }
}
