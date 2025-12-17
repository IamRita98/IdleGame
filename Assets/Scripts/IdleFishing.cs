using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IdleFishing : MonoBehaviour
{
    public float baseTimeToFinishGathering;
    public int baseGatherAmount;
    float timeToFinishGathering;
    float timer;
    bool startGathering;
    string functionToRun;
    Stats stats;
    playerItemManager playerItemManager;

    private void Awake()
    {
        stats = GetComponent<Stats>();
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

        }
    }

    void DetermineSkillingType(GameObject objectFished)
    {
        IdleSkillingStats objectsStats = objectFished.GetComponent<IdleSkillingStats>();
        baseTimeToFinishGathering = objectsStats.baseHarvestTime;
        baseGatherAmount = objectsStats.baseHarvestAmount;
        if (objectsStats.currentSkillType == SkillingType.fishing) Fish(); //Determine Fishing Object?
    }

    void Fish()
    {
        int fishingStat = stats.fishingLevel;
        timeToFinishGathering = fishingStat + baseTimeToFinishGathering;
        startGathering = true;
    }

    float levelScalingOnGatherSpeed(int level)
    {
        //int scalingModifier = 50; 
        //float levelScaled = scalingModifier / level;
        return 0;
    }
}
