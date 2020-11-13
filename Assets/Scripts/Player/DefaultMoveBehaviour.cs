using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultMoveBehaviour : MoveBehaviour
{
    public float jumpForce = 3f;


    public void Run(Rigidbody2D rb)
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);
    }

    public void Jump(Rigidbody2D rb)
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    public override void Move(Rigidbody2D rb)
    {
        Run(rb);
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Jump(rb);
        } 
    }

}
