using UnityEngine;

public class BarricadeManager : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

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
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime, Space.World);

        if (transform.position.z > 80)
        {
            Destroy(gameObject);
        }
    }
}
