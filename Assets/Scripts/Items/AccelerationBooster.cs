using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerationBooster : MonoBehaviour
{
    private float durationTime = 10f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            BoostManager.Instance.StartAccelerationBoost(durationTime);
            Destroy(gameObject);
        }
    }
}
