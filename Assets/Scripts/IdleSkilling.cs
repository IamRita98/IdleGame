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
    bool isSkilling = false;
    float progressTimer;
    GameObject skillingLocationGO;
    public bool wantToApplyMinigameBonus = false;
    FishingMinigame fishingMinigame;
    GatheringSpot gatheringSpot;


    private void Awake()
    {
        stats = GetComponent<Stats>();
        playerMovement = GetComponent<PlayerMovement>();
        playerItemManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerItemManager>();
        uiManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<UIManager>();
    }

    private void Update()
    {
        if (!isSkilling) return;
        if (playerMovement.currentState != PlayerState.skilling)
        {
            StopSkilling();
        }
        progressTimer += Time.deltaTime;
        if (wantToApplyMinigameBonus) uiManager.ApplyMinigameBonusVFX();
        else uiManager.UnapplyMinigameBonusVFX();
        uiManager.UpdateSkillingBar(progressTimer, timeToFinishGathering);
    }
    
    public void DetermineSkillingType(GameObject skillingGO)
    {
        skillingLocationGO = skillingGO;
        playerMovement.currentState = PlayerState.skilling;
        gatheringSpot = skillingGO.GetComponent<GatheringSpot>();
        baseTimeToFinishGathering = gatheringSpot.baseHarvestTime;
        baseGatherAmount = gatheringSpot.baseHarvestAmount;

        switch (gatheringSpot.tag)
        {
            case ("FishingSpot"):
                Fish(); break;
        }
    }

    void Fish()
    {
        uiManager.FishingUI();
        fishingMinigame = GameObject.FindGameObjectWithTag("FishingMinigame").GetComponent<FishingMinigame>();
        int fishingStat = stats.fishingLevel;
        timeToFinishGathering = CalcTimeToFinishGathering(fishingStat);
        int itemRoll = Random.Range(0, 100);
        itemToGather = gatheringSpot.GetItemFromPool(itemRoll);
        StartCoroutine(Idleing(timeToFinishGathering));
    }

    float CalcTimeToFinishGathering(int level)
    {
        if (wantToApplyMinigameBonus) baseTimeToFinishGathering *= gatheringSpot.minigameBonusToApply;
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
        isSkilling = true;
        yield return new WaitForSeconds(timeToIdle);
        
        GivePlayerItem(itemToGather);
    }

    void StopSkilling()
    {
        isSkilling = false;
        StopAllCoroutines();
        uiManager.StopSkillingUI();
        playerMovement.currentState = PlayerState.idle;
    }
}
