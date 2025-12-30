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
    Item itemToGather;
    bool isSkilling = false;
    float progressTimer;
    GameObject skillingLocationGO;
    public bool wantToApplyMinigameBonus = false;
    FishingMinigame fishingMinigame;
    GatheringSpot gatheringSpot;
    string currentSkillEngagedWith;


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
        if (playerMovement.currentState != PlayerState.skilling) StopSkilling();

        //UpdateProgress of skilling
        if (!wantToApplyMinigameBonus) progressTimer += Time.deltaTime;
        else progressTimer += Time.deltaTime * gatheringSpot.minigameBonusToApply;
        uiManager.UpdateSkillingBar(progressTimer, timeToFinishGathering);

        if (progressTimer >= timeToFinishGathering)
        {
            GivePlayerXP();
            GivePlayerItem(itemToGather);

            //Loop skilling from the start, can maybe make this a bit more efficient since we already know what skill
            //player is engaged w/ but I don't think it's a big deal rn
            DetermineSkillingType(skillingLocationGO);
        }
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
                currentSkillEngagedWith = "Fishing"; Fish(); break;
        }
    }

    void Fish()
    {
        uiManager.FishingUI();
        fishingMinigame = GameObject.FindGameObjectWithTag("FishingMinigame").GetComponent<FishingMinigame>();
        int fishingStat = stats.fishingLevel;
        timeToFinishGathering = CalcTimeToFinishGathering(fishingStat);
        itemToGather = gatheringSpot.GetItemFromPool();
        progressTimer = 0;
        isSkilling = true;
    }

    float CalcTimeToFinishGathering(int level)
    {
        float reductionFromLevel = level * .1f;
        float timeToFinishGathering = baseTimeToFinishGathering - reductionFromLevel; //rly basic formula that reduces the time by min to max miliseconds(1-99 for min-maxlvl?)
        return timeToFinishGathering;
    }

    void GivePlayerItem(Item obtainedItem)
    {
        itemToGather = null;
        print("+1 " + obtainedItem.name);
        playerItemManager.inventory.Add(obtainedItem);
    }

    void GivePlayerXP()
    {
        switch (currentSkillEngagedWith)
        {
            case "Fishing":
                stats.GainFishingXP(itemToGather.xpToGainOnGather); break;
        }
    }

    void StopSkilling()
    {
        isSkilling = false;
        uiManager.StopSkillingUI();
        playerMovement.currentState = PlayerState.idle;
    }
}
