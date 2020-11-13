using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MoveBehaviour : MonoBehaviour
{
    public float speed = 5f;
    public abstract void Move(Rigidbody2D rb);
}
