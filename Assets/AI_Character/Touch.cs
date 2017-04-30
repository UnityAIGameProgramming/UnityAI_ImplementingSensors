using UnityEngine;

public class Touch : Sense {

    private void OnTriggerEnter(Collider other)
    {
        Aspect aspect = other.GetComponent<Aspect>();
        if(aspect != null)
        {
            // Check the aspect
            if(aspect.aspectName == aspectName)
            {
                Debug.Log("Enemy Touch Detected");
            }
        }
    }
}
