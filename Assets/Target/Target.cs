using UnityEngine;

public class Target : MonoBehaviour
{
    public Transform targetMarker;

    // Update is called once per frame
    void Update()
    {
        int button = 0;

        // Get the point of the hit position when the mouse is being clicked
        if (Input.GetMouseButtonDown(button))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray.origin, ray.direction, out hitInfo))
            {
                Vector3 targetPosition = hitInfo.point;
                targetPosition.y = 0.5f;    // added to ensure light is always visible
                targetMarker.position = targetPosition;
            }
        }
    }
}
