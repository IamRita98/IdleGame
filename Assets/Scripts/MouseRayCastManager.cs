using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseRayCastManager : MonoBehaviour
{
    Camera mainCamera;

    public static event System.Action<Vector3> PlayerWantsToMove;

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
            if(hit.collider.CompareTag("Ground"))
            {
                PlayerWantsToMove?.Invoke(hit.point);
            }
        }
    }
}
