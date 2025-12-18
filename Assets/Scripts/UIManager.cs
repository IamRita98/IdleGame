using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider skillingProgressBar;

    private void Start()
    {
        skillingProgressBar.gameObject.SetActive(false);
    }
    public void FishingUI()
    {
        skillingProgressBar.gameObject.SetActive(true);
        //Show Minigame
    }

    public void UpdateSkillingBar(float currentTimer, float totalLength)
    {
        float percentComplete = currentTimer / totalLength;
        skillingProgressBar.value = percentComplete;
    }

    public void StopSkillingUI()
    {
        skillingProgressBar.gameObject.SetActive(false);
    }
}
