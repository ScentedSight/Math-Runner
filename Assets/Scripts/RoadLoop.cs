using UnityEngine;

public class RoadLoop : MonoBehaviour
{
    public float loopSpeed = 30f;
    public static float leftLaneX;
    public static float rightLaneX;
    private Vector3 startPos;
    private float repeatWidth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Finding x value from one end of the ground to another
        BoxCollider boxCoordinates = GetComponent<BoxCollider>();
        float leftEdge = boxCoordinates.bounds.max.x;
        float rightEdge = boxCoordinates.bounds.min.x;
        //Setting definite path for player's input traversal
        leftLaneX = leftEdge / 2f;
        rightLaneX = rightEdge / 2f;
        startPos = transform.position;
        repeatWidth = GetComponent<BoxCollider>().bounds.size.z / 2;
    }

    // Update is called once per frame
    void Update()
    {
        //Looping the floor towards the player by repeatWidth
        transform.Translate(Vector3.forward * loopSpeed * Time.deltaTime);

        if (transform.position.z > repeatWidth)
        {
            transform.position = startPos;
        }
    }
}
