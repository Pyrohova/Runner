using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcceleratedMoveBehaviour : MoveBehaviour
{
    public override void Move(Rigidbody2D rb)
    {
        rb.velocity = new Vector2(25f, rb.velocity.y);
    }
}
