using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public new Rigidbody2D rigidbody;
    public float speed;

    private Vector2 movement;

    void Start()
    {

    }

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

        Debug.Log(movement);
        Debug.Log(speed);
        Debug.Log(Time.deltaTime);
        Debug.Log(newPos);
    }

}
