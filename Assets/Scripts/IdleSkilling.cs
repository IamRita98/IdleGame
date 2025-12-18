using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IdleSkilling : MonoBehaviour
{
    public float baseTimeToFinishGathering;
    public int baseGatherAmount;
    float timeToFinishGathering;
    float timer;
    bool startGathering;
    Stats stats;
    PlayerItemManager playerItemManager;
    public float totalBonusFromMaxLevel;
    Item itemToGather;

    private void Awake()
    {
        stats = GetComponent<Stats>();
        playerItemManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerItemManager>();
    }

    private void OnEnable()
    {
        SkillingManager.PlayerIsFishing += DetermineSkillingType;
    }
    private void OnDisable()
    {
        SkillingManager.PlayerIsFishing -= DetermineSkillingType;
    }

    private void Update()
    {
        if (!startGathering) return;
        timer += Time.deltaTime;
        if(timer >= timeToFinishGathering) 
        {
            GivePlayerItem(itemToGather);
        }
    }

    void DetermineSkillingType(GameObject fishingSpotGO)
    {
        GatheringSpot gatheringSpot = fishingSpotGO.GetComponent<GatheringSpot>();
        baseTimeToFinishGathering = gatheringSpot.baseHarvestTime;
        baseGatherAmount = gatheringSpot.baseHarvestAmount;
        if (gatheringSpot.gatheringSpotType == GatheringSpotType.Fishing) Fish(gatheringSpot); //Determine Fishing Object?
    }

    void Fish(GatheringSpot gatheringSpot)
    {
        int fishingStat = stats.fishingLevel;
        timeToFinishGathering = CalcTimeToFinish(fishingStat);
        startGathering = true;
        int itemRoll = Random.Range(0, 100);
        itemToGather = gatheringSpot.GetItemFromPool(itemRoll);
    }

    float CalcTimeToFinish(int level)
    {
        float reductionFromLevel = level * .1f;
        float timeToFinishGathering = baseTimeToFinishGathering - reductionFromLevel; //rly basic formula that reduces the time by min to max seconds(1-99 for min-maxlvl?)
        return timeToFinishGathering;
    }

    void GivePlayerItem(Item obtainedItem)
    {
        startGathering = false;
        timer = 0;
        itemToGather = null;

        playerItemManager.inventory.Add(obtainedItem);
    }
}
