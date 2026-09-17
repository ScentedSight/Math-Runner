using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float jumpForce;
    private Rigidbody rb;
    private bool grounded = true;
    public float changeLaneSpeed = 10f;
    private float targetX;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        // Player initially wants to remain wherever it currently is.
        targetX = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = new Vector3(targetX, transform.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, changeLaneSpeed * Time.deltaTime); 

        if (Keyboard.current.aKey.wasPressedThisFrame)
        {
            DashLeft();
        }
        else if (Keyboard.current.dKey.wasPressedThisFrame)
        {
            DashRight();
        }

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Jump();
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

    public void DashLeft()
    {
        targetX = RoadLoop.leftLaneX;
    }

    public void DashRight()
    {
        targetX = RoadLoop.rightLaneX;
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
            GetComponent<GameManager>().GameOver();
        }  
    }
}
