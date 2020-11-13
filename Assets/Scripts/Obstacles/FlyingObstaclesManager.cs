using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingObstaclesManager : MonoBehaviour
{
    public static FlyingObstaclesManager Instance { get; private set; }

    [SerializeField]
    GameObject obstacle;

    private bool spawned;
    private bool obstaclesAreDisabled = false;

    public void SpawnObstacle()
    {
        spawned = false;

        if (!spawned && !obstaclesAreDisabled)
        {
            Instantiate(obstacle);
            spawned = true;
        }
    }

    private void CreateSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void InitializeManager()
    {
        GameWorldManager.Instance.DynamicObstaclesDisabled += () => { obstaclesAreDisabled = true; };
        GameWorldManager.Instance.DynamicObstaclesEnabled += () => { obstaclesAreDisabled = false; };
    }

    private void Awake()
    {
        CreateSingleton();
        InitializeManager();
    }
}