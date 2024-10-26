using UnityEngine;

public class FollowTransform : MonoBehaviour
{
    [SerializeField] private Transform target; // The Transform to follow

    void Update()
    {
        if (target != null)
        {
            // Update the position to match the target's position
            transform.position = target.position;

            // Optionally, update the rotation to match the target's rotation
            //transform.rotation = target.rotation;

            // Optionally, update the scale to match the target's scale
            transform.localScale = target.localScale;
        }
    }
}
