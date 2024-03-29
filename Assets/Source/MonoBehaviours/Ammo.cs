﻿using UnityEngine;

class Ammo : MonoBehaviour
{

    public Vector2 DeltaMovement { get; set; }

    private void FixedUpdate()
    {
        Vector2 newPos = (Vector2)transform.position + DeltaMovement * Time.deltaTime;
        GetComponent<Rigidbody2D>().MovePosition(newPos);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var hitable = collision.gameObject.GetComponent<Hitable>();
        if (hitable == null || hitable.Type != ObjectType.Player)
            Destroy(gameObject);
    }

}
