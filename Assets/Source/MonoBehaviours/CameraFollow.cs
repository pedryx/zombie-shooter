using UnityEngine;


/// <summary>
/// Represent a component which will allow camera to follow a target entity.
/// </summary>
public class CameraFollow : MonoBehaviour
{
    /// <summary>
    /// Target entity which will be followed by a camera.
    /// </summary>
    public Transform Target;

    void LateUpdate()
    {
        transform.position = new Vector3()
        {
            x = Target.position.x,
            y = Target.position.y,
            z = transform.position.z
        };
    }
}
