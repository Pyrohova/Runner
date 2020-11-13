using UnityEngine;


public static class PlayerDataHolder
{

    public static int GetCoins()
    {
        return PlayerPrefs.GetInt("Coins");
    }

    public static void SetCoins(int coins)
    {
        PlayerPrefs.SetInt("Coins", coins);
        PlayerPrefs.Save();
    }

    public static int GetLives()
    {
        return PlayerPrefs.GetInt("Lives");
    }

    public static void SetLives(int lives)
    {
        PlayerPrefs.SetInt("Lives", lives);
        PlayerPrefs.Save();
    }

    public static void SetItem(Item item, int value)
    {
        PlayerPrefs.SetInt(item.ToString(), value);
    }

    public static int GetItemAmount(Item item)
    {
        return PlayerPrefs.GetInt(item.ToString());
    }

    public static void IncrementItem(Item item)
    {
        PlayerPrefs.SetInt(item.ToString(), GetItemAmount(item) + 1);
        PlayerPrefs.Save();
    }

    public static bool DecrementItem(Item item)
    {
        if (GetItemAmount(item) > 0)
        {
            PlayerPrefs.SetInt(item.ToString(), GetItemAmount(item) - 1);
            PlayerPrefs.Save();
            return true;
        }
        return false;
    }
}
