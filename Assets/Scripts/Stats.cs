using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Stats : MonoBehaviour
{
    const string FISHING = "Fishing";
    public int maxLevel = 99;
    public int minLevel = 1;
    public int fishingLevel;
    public float fishingXP = 0;
    public int fishingXPToNextLevel;

    private void Start()
    {
        fishingXPToNextLevel = fishingLevel * 20; //Basic starting formula for leveling
    }

    public void GainFishingXP(float xpToGain)
    {
        fishingXP += xpToGain;
        print("+" + xpToGain + "XP! " +  fishingXP + " Total XP");
        if (fishingXP >= fishingXPToNextLevel) LevelUp(FISHING, fishingLevel);
    }

    void LevelUp(string skill, int skillLevel)
    {
        switch (skill)
        {
            case "Fishing":
                fishingXP = 0; fishingLevel++; fishingXPToNextLevel = fishingLevel * 20; break;
        }
        print("Level up! " + skill + " Level: " + fishingLevel);
    }
}
