using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IdleSkilling : MonoBehaviour
{
    public float baseTimeToFinishGathering;
    public int baseGatherAmount;
    float timeToFinishGathering;
    Stats stats;
    PlayerItemManager playerItemManager;
    UIManager uiManager;
    PlayerMovement playerMovement;
    public float totalBonusFromMaxLevel;
    Item itemToGather;
    bool sendToUI = false;
    float progressTimer;
    GameObject skillingLocationGO;

    private void Awake()
    {
        stats = GetComponent<Stats>();
        playerMovement = GetComponent<PlayerMovement>();
        playerItemManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerItemManager>();
        uiManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<UIManager>();
    }

    private void Update()
    {
        if (!sendToUI) return;
        if (playerMovement.currentState != PlayerState.skilling)
        {
            StopSkilling();
        }
        progressTimer += Time.deltaTime;
        uiManager.UpdateSkillingBar(progressTimer, timeToFinishGathering);
    }

    public void DetermineSkillingType(GameObject skillingGO)
    {
        skillingLocationGO = skillingGO;
        playerMovement.currentState = PlayerState.skilling;
        GatheringSpot gatheringSpot = skillingGO.GetComponent<GatheringSpot>();
        baseTimeToFinishGathering = gatheringSpot.baseHarvestTime;
        baseGatherAmount = gatheringSpot.baseHarvestAmount;

        switch (gatheringSpot.tag)
        {
            case ("FishingSpot"):
                Fish(gatheringSpot); break;
        }
    }

    void Fish(GatheringSpot gatheringSpot)
    {
        uiManager.FishingUI();
        int fishingStat = stats.fishingLevel;
        timeToFinishGathering = CalcTimeToFinishGathering(fishingStat);
        int itemRoll = Random.Range(0, 100);
        itemToGather = gatheringSpot.GetItemFromPool(itemRoll);
        StartCoroutine(Idleing(timeToFinishGathering));
    }

    float CalcTimeToFinishGathering(int level)
    {
        float reductionFromLevel = level * .1f;
        float timeToFinishGathering = baseTimeToFinishGathering - reductionFromLevel; //rly basic formula that reduces the time by min to max seconds(1-99 for min-maxlvl?)
        return timeToFinishGathering;
    }

    void GivePlayerItem(Item obtainedItem)
    {
        itemToGather = null;

        print("+1 " + obtainedItem.name);
        playerItemManager.inventory.Add(obtainedItem);
        DetermineSkillingType(skillingLocationGO);
    }

    IEnumerator Idleing(float timeToIdle)
    {
        progressTimer = 0;
        sendToUI = true;
        yield return new WaitForSeconds(timeToIdle);
        
        GivePlayerItem(itemToGather);
    }

    void StopSkilling()
    {
        sendToUI = false;
        StopAllCoroutines();
        uiManager.StopSkillingUI();
        playerMovement.currentState = PlayerState.idle;
    }
}
