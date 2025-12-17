using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillingManager : MonoBehaviour
{
    /// <summary>
    /// Called when player starts Fishing
    /// </summary>
    public static event System.Action<GameObject> PlayerIsFishing;

    public void CheckSkillToStart(GameObject skillToStart)
    {
        switch (skillToStart.tag)
        {
            case ("FishingSpot"):
                Fish(skillToStart); break;
        }
    }

    public void Fish(GameObject skillStarted)
    {
        PlayerIsFishing?.Invoke(skillStarted);
        print("Fishing");
    }
}
