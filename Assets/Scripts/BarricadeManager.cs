using UnityEngine;

public class BarricadeManager : MonoBehaviour
{
    private float moveSpeed;

    void Awake()
    {
        RoadLoop roadLoop = FindAnyObjectByType<RoadLoop>();
        if (roadLoop != null)
        {
            moveSpeed = roadLoop.loopSpeed * 2.5f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Moving the barricade towards the player
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime, Space.World);
        //Destroy out of bound objects
        if (transform.position.z > 80)
        {
            Destroy(gameObject);
        }
    }
}
