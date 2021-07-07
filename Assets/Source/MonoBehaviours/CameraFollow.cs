using UnityEngine;


/// <summary>
/// Represent a component which will allow camera to follow a target entity.
/// </summary>
public class CameraFollow : MonoBehaviour
{
    /// <summary>
    /// Target entity which will be followed by a camera.
    /// </summary>
    public Transform target;

    void LateUpdate()
    {
        transform.position = new Vector3()
        {
            x = target.position.x,
            y = target.position.y,
            z = transform.position.z
        };
    }
}
