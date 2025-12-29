using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider skillingProgressBar;
    public GameObject fishingUIGO;
    GameObject currentSkillingUI;
    IdleSkilling idleSkilling;
    public Image progressBarFill;

    private void Start()
    {
        idleSkilling = GameObject.FindGameObjectWithTag("Player").GetComponent<IdleSkilling>();
        skillingProgressBar.gameObject.SetActive(false);
    }

    public void FishingUI()
    {
        skillingProgressBar.gameObject.SetActive(true);
        fishingUIGO.SetActive(true);
        currentSkillingUI = fishingUIGO;
    }

    public void UpdateSkillingBar(float currentTimer, float totalLength)
    {
        float percentComplete = currentTimer / totalLength;
        skillingProgressBar.value = percentComplete;
    }

    public void StopSkillingUI()
    {
        skillingProgressBar.gameObject.SetActive(false);
        currentSkillingUI.SetActive(false);
    }

    //Color progressBarBoostedRGBA = new Color(210, 200, 40, 225);
    public void ApplyMinigameBonusVFX()
    {
        progressBarFill.color = Color.yellow;
    }

    //Color progressBarOrginalRGBA = new Color(255, 255, 255, 225);
    public void UnapplyMinigameBonusVFX()
    {
        progressBarFill.color = Color.white;
    }
}
