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

    public void ApplyMinigameBonusVFX()
    {
        
        Vector4 progressBarBoostedRGBA = new Vector4(210, 130, 40, 225);
        progressBarFill.color = progressBarBoostedRGBA;
    }

    public void UnapplyMinigameBonusVFX()
    {
        Vector4 progressBarOrginalRGBA = new Vector4(210, 205, 40, 225);
        progressBarFill.color = progressBarOrginalRGBA;
    }
}
