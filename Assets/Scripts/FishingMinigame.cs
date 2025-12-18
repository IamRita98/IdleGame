using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class FishingMinigame : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        print("Cum");
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        print("No Cum");
    }
}
