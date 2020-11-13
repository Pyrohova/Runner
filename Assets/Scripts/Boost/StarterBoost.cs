using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarterBoost : Boost
{
    private float durationTime = 10f;

    public override void Apply()
    {
        if (!BoostManager.Instance.BoostIsRunning)
        {
            SetAcceleratedBoostMove();

            PlayerDataHolder.DecrementItem(BoostItem);
        }
    }

    private void SetAcceleratedBoostMove()
    {
        BoostManager.Instance.BoostIsRunning = true;
        BoostManager.Instance.playerController.SetModifiedMove(new AcceleratedMoveBehaviour());

        StartCoroutine(BoostDuration());
    }

    private IEnumerator BoostDuration()
    {
        GameWorldManager.Instance.DisableObstacles();
        yield return new WaitForSeconds(durationTime);

        BoostManager.Instance.playerController.SetDefaultMove();
        GameWorldManager.Instance.EnableObstacles();

        BoostManager.Instance.BoostIsRunning = false;
    }

    private void Awake()
    {
        BoostItem = Item.AccelerationBooster;
        MaxUsePerLevel = 1;
    }
}
