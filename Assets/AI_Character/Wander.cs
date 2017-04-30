using UnityEngine;

public class Wander : MonoBehaviour
{
    private Vector3 tarPos;

    private float movementSpeed = 5.0f;
    private float rotSpeed = 2.0f;
    private float minX, maxX, minZ, maxZ;

    // Use this for initialization
    void Start()
    {
        minX = -45.0f;
        maxX = 45.0f;

        minZ = -45.0f;
        maxZ = 45.0f;

        // Get wander position
        GetNextPosition();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if we're near the destination position
        if (Vector3.Distance(tarPos, transform.position) <= 5.0f)
            GetNextPosition();  // generate new random position

        // Set up quaternion for rotation toward destination
        Quaternion tarRot = Quaternion.LookRotation(tarPos - transform.position);

        // Update rotation and translation
        transform.rotation = Quaternion.Slerp(transform.rotation, tarRot, rotSpeed * Time.deltaTime);

        transform.Translate(new Vector3(0, 0, movementSpeed * Time.deltaTime));
    }

    void GetNextPosition()
    {
        tarPos = new Vector3(Random.Range(minX, maxX), 0.5f, Random.Range(minZ, maxZ));
    }
}
