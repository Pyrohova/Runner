using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public GameObject staticObstacles;
    public GameObject dynamicObstacles;

    public void DisableObstacles()
    {
        staticObstacles?.SetActive(false);
        dynamicObstacles?.SetActive(false);
    }

    public void EnableObstacles()
    {
        staticObstacles?.SetActive(true);
        dynamicObstacles?.SetActive(true);
    }

    public void DisableStaticObstacles()
    {
        staticObstacles?.SetActive(false);
    }

    public void DisableDynamicObstacles()
    {
        dynamicObstacles?.SetActive(false);
    }

}
