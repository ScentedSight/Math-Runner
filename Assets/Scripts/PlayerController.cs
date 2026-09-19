using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 1250f;
    public float changeLaneSpeed = 35f;
    public int currentLane;
    public int health = 1;
    private float targetX;
    private Rigidbody rb;
    private bool grounded = true;
    private Quaternion carDefaultRotation;
    [SerializeField] private float gravity = -40f;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Transform carModel;
    [SerializeField] private float turnAngle = 20f;
    [SerializeField] private float rotationSpeed = 8f;

    void Awake()
    {
        Physics.gravity = new Vector3(0f, gravity, 0f); //Custom gravity to configure jump's curve
        rb = GetComponent<Rigidbody>();
        // Player initially wants to remain wherever it currently is.
        targetX = transform.position.x;
        // Remember whatever rotation you set in the Inspector
        carDefaultRotation = carModel.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = new Vector3(targetX, transform.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, changeLaneSpeed * Time.deltaTime); 

        // Determine which direction we're moving
        float difference = targetX - transform.position.x;
        float targetAngle = 0f;

        if (Mathf.Abs(difference) > 0.01f)
        {
            targetAngle = difference < 0 ? turnAngle : -turnAngle;
        }

        Quaternion steeringRotation = Quaternion.Euler(0f, targetAngle, 0f);
        Quaternion targetRotation = steeringRotation * carDefaultRotation;
        carModel.localRotation = Quaternion.Lerp(carModel.localRotation, targetRotation, rotationSpeed * Time.deltaTime);

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
        //Check if grounded first to avoid double jumping
        if (grounded == true)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            grounded = false;
        } 
    }

    public void DashLeft()
    {
        currentLane = 0;
        targetX = RoadLoop.leftLaneX;
    }

    public void DashRight()
    {
        currentLane = 1;
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
        if (other.CompareTag("Obstacle") && health == 1)
        {
            gameManager.GameOver();
        }
        else if (other.CompareTag("Obstacle"))
        {
            health --;
        }

        if (other.CompareTag("PowerUp"))
        {
            health++;
            Destroy(other.gameObject);
        }
    }
}
