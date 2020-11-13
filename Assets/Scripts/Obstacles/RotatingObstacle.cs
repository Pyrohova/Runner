using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingObstacle : Obstacle
{
    private float speed = 250f;

    private void Rotate()
    {
        transform.Rotate(new Vector3(0, 0, Time.deltaTime * speed));
    }

    private void Update()
    {
        Rotate();
    }
}
