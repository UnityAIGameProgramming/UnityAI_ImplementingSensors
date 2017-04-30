using UnityEngine;

public class Perspective : Sense
{
    public int FieldOfView = 45;
    public int ViewDistance = 100;

    private Transform playerTrans;
    private Vector3 rayDirection;

    protected override void Initialize()
    {
        // Find player position
        playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    protected override void UpdateSense()
    {
        elapsedTime += Time.deltaTime;

        // Detect perspective sense if within the detection rate
        if (elapsedTime >= detectionRate)
            DetectAspect();
    }

    // Detect perspective field of view for the AI Character
    void DetectAspect()
    {
        RaycastHit hit;

        // Direction from current position to player position
        rayDirection = playerTrans.position - transform.position;

        // Check the angle between the AI character's forward vector and the direction 
        // vector between player and AI
        if ((Vector3.Angle(rayDirection, transform.forward)) < FieldOfView)
        {
            // Detect if player is within the field of view
            if (Physics.Raycast(transform.position, rayDirection, out hit, ViewDistance))
            {
                Aspect aspect = hit.collider.GetComponent<Aspect>();

                if (aspect != null)
                {
                    // Check the aspect
                    if (aspect.aspectName == aspectName)
                    {
                        Debug.Log("Enemy Detected");
                    }
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (playerTrans == null) return;

        Debug.DrawLine(transform.position, playerTrans.position, Color.red);

        // Vector3 frontRayPoint = transform.position + (transform.forward * ViewDistance);

        //    // Approximate perspective visualisation
        //    Vector3 leftRayPoint = frontRayPoint;
        //    leftRayPoint.x += FieldOfView * 0.5f;

        //    Vector3 rightRayPoint = frontRayPoint;
        //    rightRayPoint.x -= FieldOfView * 0.5f;

        //    Debug.DrawRay(transform.position, frontRayPoint, Color.blue);
        //    Debug.DrawLine(transform.position, leftRayPoint, Color.green);
        //    Debug.DrawLine(transform.position, rightRayPoint, Color.green);

        // http://answers.unity3d.com/questions/21176/gizmo-question-how-do-i-create-a-field-of-view-usi.html

        //float totalFOV = 70.0f;
        // float rayRange = 10.0f;

        float halfFOV = FieldOfView / 2.0f;
        Quaternion frontRayRotation = Quaternion.identity;

        Quaternion leftRayRotation = Quaternion.AngleAxis(-halfFOV, Vector3.up);
        Quaternion rightRayRotation = Quaternion.AngleAxis(halfFOV, Vector3.up);

        Vector3 leftRayDirection = leftRayRotation * transform.forward;
        Vector3 rightRayDirection = rightRayRotation * transform.forward;

        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, leftRayDirection * ViewDistance);
        Gizmos.DrawRay(transform.position, rightRayDirection * ViewDistance);

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, transform.forward * ViewDistance);


    }
}
