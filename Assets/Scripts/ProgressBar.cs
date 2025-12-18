using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    Slider progressBar;
    public bool updateBar = false;

    private void Awake()
    {
        progressBar = GetComponent<Slider>();
    }

    public void UpdateBar(float currentTimer, float totalLength)
    {
        float percentComplete = currentTimer / totalLength;
        progressBar.value = percentComplete;
    }
}
