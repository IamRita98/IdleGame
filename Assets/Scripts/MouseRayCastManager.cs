using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseRayCastManager : MonoBehaviour
{
    Camera mainCamera;
    /// <summary>
    /// Called when player clicks on the ground. Passes click pos
    /// </summary>
    public static event System.Action<Vector3> PlayerWantsToMove;

    /// <summary>
    /// Called when player clicks on a skilling spot. Passes GO clicked on & Click pos
    /// </summary>
    public static event System.Action<GameObject, Vector3> PlayerWantsToSkill; 
    //This might be unnesecary if we end up only using it in 1 place. This and playerWantsToMove both go to the same place too.

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) DetectClickedObject();
    }

    void DetectClickedObject()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if(hit.collider.CompareTag("Ground")) PlayerWantsToMove?.Invoke(hit.point);
            if (hit.collider.CompareTag("FishingSpot")) PlayerWantsToSkill?.Invoke(hit.collider.gameObject, hit.point);
        }
    }
}
