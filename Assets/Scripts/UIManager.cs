using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider progressBar;

    private void OnEnable()
    {
        SkillingManager.PlayerIsFishing += FishingUI;
    }
    private void OnDisable()
    {
        SkillingManager.PlayerIsFishing -= FishingUI;
    }

    void FishingUI()
    {
        progressBar.enabled = true;
        //Show Minigame
    }
}
