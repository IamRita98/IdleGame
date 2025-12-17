using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillingType
{
    fishing,
    mining,
    logging
}
public class IdleSkillingStats : MonoBehaviour
{
    public SkillingType currentSkillType;
    public string nameOfResource;
    public float baseHarvestTime;
    public int baseHarvestAmount;
}
