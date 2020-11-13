using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopBoostItemsManager : MonoBehaviour
{
    public static ShopBoostItemsManager Instance { get; private set; }

    public IEnumerable<Boost> boughtBoosters;

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
        boughtBoosters = GetComponents<Boost>();
    }

    void Awake()
    {
        CreateSingleton();
        InitializeManager();
    }
}
