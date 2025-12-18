using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GatheringSpotType
{
    Fishing,
    Mining,
    Logging,
}
public class GatheringSpot : MonoBehaviour
{
    int levelToHarvest;
    public float baseHarvestTime;
    public int baseHarvestAmount;
    public GatheringSpotType gatheringSpotType;
    

    public List<Item> poolOfMaterials = new List<Item>();
    /// <summary>
    /// The final entry will cover the rest of the 100% that was left unspecified
    /// </summary>
    public List<int> oddsForEachItemInPool = new List<int>();

    /// <summary>
    /// Pass a random number 1-100 and get back an item from the pool
    /// </summary>
    /// <param name="randomNumberRolled"></param>
    /// <returns></returns>
    public Item GetItemFromPool(int randomNumberRolled)
    {
        Item itemRolledFromPool = null;
        int firstNum = 0;
        int secondNum = oddsForEachItemInPool[0];
        for (int i = 0; i < poolOfMaterials.Count; i++)
        {
            if (poolOfMaterials[i + 1] == null) return poolOfMaterials[i];
            if (randomNumberRolled >= firstNum && randomNumberRolled < secondNum) return poolOfMaterials[i];
            firstNum = oddsForEachItemInPool[i];
            secondNum = oddsForEachItemInPool[i + 1];
        }
        return itemRolledFromPool;
    }
}
