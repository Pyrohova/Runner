using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject player;
    private Vector3 lastPosition;
    private float distance;

    void MoveWithPlayer()
    {
        transform.position = new Vector3(transform.position.x + distance, transform.position.y, transform.position.z);
    }

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        lastPosition = player.transform.position;
    }


    void Update()
    {
        distance = player.transform.position.x - lastPosition.x;
        MoveWithPlayer();
        lastPosition = player.transform.position;
    }
}
