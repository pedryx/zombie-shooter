using UnityEngine;


/// <summary>
/// Represent a component which allows entity to be controlled by a player.
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    /// <summary>
    /// Represent a current movement vector.
    /// </summary>
    private Vector2 movement;
    /// <summary>
    /// Rigidbody which will be controlled.
    /// </summary>
    private new Rigidbody2D rigidbody;

    /// <summary>
    /// Motion speed.
    /// </summary>
    public float Speed;

    private void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        Vector2 newPos = (Vector2)transform.position + movement * Speed * Time.deltaTime;
        rigidbody.MovePosition(newPos);
    }

}
