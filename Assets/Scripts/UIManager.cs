using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject skillingProgressBar;

    private void OnEnable()
    {
        SkillingManager.PlayerIsFishing += FishingUI;
    }
    private void OnDisable()
    {
        SkillingManager.PlayerIsFishing -= FishingUI;
    }

    void FishingUI(GameObject goFished)
    {
        skillingProgressBar.SetActive(true);
        //Show Minigame
    }

    void StopSkillingUI()
    {
        skillingProgressBar.SetActive(false);
    }
}
