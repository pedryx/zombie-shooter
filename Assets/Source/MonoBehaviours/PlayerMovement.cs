using UnityEngine;


/// <summary>
/// Represent a component which allows entity to be controlled by a player.
/// </summary>
public class PlayerMovement : MonoBehaviour
{

    /// <summary>
    /// Rigidbody which will be controlled.
    /// </summary>
    public new Rigidbody2D rigidbody;
    /// <summary>
    /// Motion speed.
    /// </summary>
    public float speed;

    /// <summary>
    /// Represent a current movement vector.
    /// </summary>
    private Vector2 movement;

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        Vector2 newPos = new Vector2()
        {
            x = transform.position.x + movement.x * speed * Time.deltaTime,
            y = transform.position.y + movement.y * speed * Time.deltaTime,
        };

        rigidbody.MovePosition(newPos);
    }

}
