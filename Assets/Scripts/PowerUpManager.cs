using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 70f;
    [SerializeField] private float rotateSpeed = 200f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime,Space.World);
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime); //Rotate the object
        //Destroy out of bound objects
        if (transform.position.z > 80)
        {
            Destroy(gameObject);
        }
    }
}
