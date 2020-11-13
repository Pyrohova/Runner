using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameWorldManager : MonoBehaviour
{
    public static GameWorldManager Instance { get; private set; }

    public event Action DynamicObstaclesDisabled;
    public event Action DynamicObstaclesEnabled;

   [SerializeField]
    Chunk startChunk;

    [SerializeField]
    Level startLevel;

    [SerializeField]
    Chunk[] chunkPrefabs;

    // Set of items and obstacles
    [SerializeField] 
    Level[] levelPrefabs;

    // Previous, current, next chunks
    private List<Chunk> existedChunks;

    // Previous, current, next levels
    private List<Level> existedLevels;

    private Vector3 newPosition;

    private bool obstaclesAreDisabled = false;

    //Called by current chunk trigger when plyaer enters the middle of chunk
    public void AddNewChunk()
    {
        //Spawn next chunk
        Chunk newChunk = Instantiate(chunkPrefabs[UnityEngine.Random.Range(0, chunkPrefabs.Length)]) as Chunk;
        newPosition = existedChunks[existedChunks.Count - 1].End.position - newChunk.Begin.localPosition;
        newChunk.transform.position = newPosition;
        existedChunks.Add(newChunk);

        //Spawn next level
        Level newLevel = Instantiate(levelPrefabs[UnityEngine.Random.Range(0, levelPrefabs.Length)]) as Level;
        newLevel.transform.position = newPosition;
        existedLevels.Add(newLevel);

        //Remove previous chunk
        if (existedChunks.Count == 3)
        {
            RemoveOldChunk();
        }
    }

    private void RemoveOldChunk()
    {
        Destroy(existedChunks[0].gameObject);
        existedChunks.RemoveAt(0);

        Destroy(existedLevels[0].gameObject);
        existedLevels.RemoveAt(0);
    }

    private void Reset()
    {
        for (int i = 0; i < existedChunks.Count; i++)
        {
            Destroy(existedChunks[i].gameObject);
            Destroy(existedLevels[i].gameObject);

        }

        existedChunks = new List<Chunk>();
        existedLevels = new List<Level>();
        RestartGame();
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

    public void DisableObstacles()
    {
        obstaclesAreDisabled = true;
        DynamicObstaclesDisabled?.Invoke();
    }

    public void EnableObstacles()
    {
        obstaclesAreDisabled = false;
        DynamicObstaclesEnabled?.Invoke();
    }

    public void OnFlyingObstacleStarted()
    {
        //Disable static obstacles
        foreach (Level level in existedLevels)
            level.DisableStaticObstacles();

    }

    private void InitializeManager()
    {
        existedChunks = new List<Chunk>();
        existedLevels = new List<Level>();
    }

    public void RestartGame()
    {
        Chunk newChunk = Instantiate(startChunk) as Chunk;
        existedChunks.Add(newChunk);

        Level newLevel = Instantiate(startLevel) as Level;
        existedLevels.Add(newLevel);
        EnableObstacles();

    }

    private void Update()
    {
        if (obstaclesAreDisabled)
        {
            foreach (Level level in existedLevels)
                level.DisableObstacles();
        }
    }

    private void Awake()
    {
        CreateSingleton();
        InitializeManager();

        GameManager.Instance.OnGameEnded += Reset;
    }

    private void Start()
    {
        RestartGame();
    }
}