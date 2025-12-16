using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleSkilling : MonoBehaviour
{
    float timeToFinishGathering;
    float timer;
    bool startGathering;
    private void OnEnable()
    {
        SkillingManager.PlayerIsFishing += IdleFishing;
    }
    private void OnDisable()
    {
        SkillingManager.PlayerIsFishing -= IdleFishing;
    }

    private void Update()
    {
        if (!startGathering) return;
        timer += Time.deltaTime;
        if(timer >= timeToFinishGathering) { }
    }

    void IdleFishing()
    {
        //timeToFinishGathering = baseGatheringTime + gatheringTimeMultOfResource - skillLevel
        //startGathering = true;
    }
}
