using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public bool IsActive { get; set; } = true;

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && IsActive)
        {
            GameManager.Instance.OnPlayerTrappedInObstacle();
        }
    }
}
