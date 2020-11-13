using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingObstacle : Obstacle
{
    private float speed = 5f;
    private Rigidbody2D rb;
    private GameObject player;

    float cameraRightPoint;

    float obstacleLength = 5f;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();

        cameraRightPoint = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)).x;
    }

    private void Start()
    {
        transform.position = new Vector3(cameraRightPoint + obstacleLength, player.transform.position.y, transform.position.z);

        StartCoroutine(FlyingObstacleBehavior());
    }

    public IEnumerator FlyingObstacleBehavior()
    {
        Vector3 direction = Vector3.left * speed;
        direction.y = rb.velocity.y;
        rb.velocity = direction;

        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}