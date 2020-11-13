using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostManager : MonoBehaviour
{
    public static BoostManager Instance { get; private set; }

    public PlayerController playerController;

    [SerializeField]
    GameObject boughtBoostersList;

    // To forbid boosters to interrupt each other
    public bool BoostIsRunning = false;

    //All bosters
    private Dictionary<Item, int> boostersInShop;

    //All bosters that can be bought
    private IEnumerable<Boost> boughtBoosters;  

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

    private bool DecrementBooster(Item item)
    {
        if (boostersInShop[item] > 0)
        {
            boostersInShop[item] -= 1;
            return true;
        }
        return false;
    }


    public void SaveBoostersIntoHoard()
    {
        foreach (Item item in boostersInShop.Keys)
        {
            PlayerDataHolder.SetItem(item, boostersInShop[item]);
        }
    }

    // Update info about amounts of boosters
    public void UpdateBoosters()
    {
        foreach (Boost boost in boughtBoosters)
        {
            boostersInShop[boost.BoostItem] = PlayerDataHolder.GetItemAmount(boost.BoostItem);
        }
    }

    private void LoadBoostersFromHoard()
    {
        foreach (Boost boost in boughtBoosters)
        {
            boostersInShop.Add(boost.BoostItem, PlayerDataHolder.GetItemAmount(boost.BoostItem));
        }
    }

    private void InitializeManager()
    {
        boughtBoosters = ShopBoostItemsManager.Instance.boughtBoosters;
        boostersInShop = new Dictionary<Item, int>();

        LoadBoostersFromHoard();
    }

    public void ApplyPossibleItemsOnStart()
    {
        UpdateBoosters();

        int allowedTimes;
        int amounLeft;
        int counter = 0;
        foreach (Boost booster in boughtBoosters)
        {
            //If there is at least one item in holder
            if (boostersInShop[booster.BoostItem] > 0)
            {
                amounLeft = boostersInShop[booster.BoostItem];

                //If there is posibility of infinity use then allowed times = amount of items in holder
                allowedTimes = (booster.MaxUsePerLevel != -1) ? booster.MaxUsePerLevel : ++amounLeft;

                //Apply while do not run out of allowed times or run out of boosters in holder
                while (allowedTimes > counter && DecrementBooster(booster.BoostItem))
                {
                    booster.Apply();
                    ++counter;
                }

            }
        }
    }

    private IEnumerator DisableObstaclesBoostDuration(float boostDuration)
    {
        BoostIsRunning = true;
        GameWorldManager.Instance.DisableObstacles();

        yield return new WaitForSeconds(boostDuration);
        GameWorldManager.Instance.EnableObstacles();

        BoostIsRunning = false;

    }

    private IEnumerator AccelerationBoostDuration(float boostDuration)
    {
        BoostIsRunning = true;
        playerController.SetModifiedMove(new AcceleratedMoveBehaviour());
        GameWorldManager.Instance.DisableObstacles();

        yield return new WaitForSeconds(boostDuration);
        playerController.SetDefaultMove();
        GameWorldManager.Instance.EnableObstacles();

        BoostIsRunning = false;
    }

    public void StartAccelerationBoost(float boostDuration)
    {
        if (!BoostIsRunning)
            StartCoroutine(AccelerationBoostDuration(boostDuration));
    }

    public void StartDisableObstaclesBoost(float boostDuration)
    {
        if (!BoostIsRunning)
            StartCoroutine(DisableObstaclesBoostDuration(boostDuration));
    }

    private void Awake()
    {
        CreateSingleton();
        InitializeManager();
    }

    private void Start()
    {
        GameManager.Instance.OnGameStarted += ApplyPossibleItemsOnStart;
        GameManager.Instance.OnGameEnded += SaveBoostersIntoHoard;
    }

}
