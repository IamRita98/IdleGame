using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillingManager : MonoBehaviour
{
    /// <summary>
    /// Called when player starts Fishing
    /// </summary>
    public static event System.Action PlayerIsFishing;

    public void CheckSkillToStart(GameObject skillToStart)
    {
        switch (skillToStart.tag)
        {
            case ("FishingSpot"):
                Fish(); break;
        }
    }

    public void Fish()
    {
        PlayerIsFishing?.Invoke();
        print("Fishing");
    }
}
