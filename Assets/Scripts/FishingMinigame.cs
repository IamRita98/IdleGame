using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class FishingMinigame : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float circleLingerTime = 10;
    public float circleVerticalBuffer = 50;
    public float circleHorizontalBuffer = 50;
    RectTransform parentPanel;

    private void OnEnable()
    {
        parentPanel = transform.parent.GetComponent<RectTransform>();
        RandomizeCirclesSpotOnMinigameScreen();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        print("Cum");
        //GiveBonusToIdleSkilling
        //ChangeUIToReflectHover(MakeCircleDarker & ChangeProgBarColor)
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        print("No Cum");
    }

    void RandomizeCirclesSpotOnMinigameScreen()
    {
        float panelWidth = 1640f;
        float panelHeight = 800f;
        float spawnPosX = Random.Range(0, panelWidth);
        float spawnPosY = Random.Range(0, panelHeight);
        PlaceCircle(spawnPosX, spawnPosY);
    }

    void PlaceCircle(float spawnPosX, float spawnPosY)
    {
        transform.position = new Vector3(spawnPosX, spawnPosY, 0);
        StartCoroutine(CircleLingeringInPlace());
    }

    IEnumerator CircleLingeringInPlace()
    {
        yield return new WaitForSeconds(circleLingerTime);
        RandomizeCirclesSpotOnMinigameScreen();
    }
}
