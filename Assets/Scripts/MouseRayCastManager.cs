using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class MouseRayCastManager : MonoBehaviour
{
    const int GROUND = 6;
    const int SKILL = 7;
    CustomAction inputAction;
    float maxRaycastDistance = 100;
    [SerializeField] LayerMask clickableLayers;
    [SerializeField] ParticleSystem clickEffect;

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
        inputAction = new CustomAction();
        AssignInputs();
    }

    private void OnEnable()
    {
        inputAction.Enable();
    }
    private void OnDisable()
    {
        inputAction.Disable();
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Mouse0)) DetectClickedObject();
    }

    void AssignInputs()
    {
        inputAction.Main.Move.performed += ctx => DetectClickedObject();
    }

    void DetectClickedObject()
    {
        RaycastHit hit;
        if(Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out hit, maxRaycastDistance, clickableLayers))
        {
            if (clickEffect != null) Instantiate(clickEffect, hit.point += new Vector3(0, .1f, 0), clickEffect.transform.rotation);
            if(hit.collider.gameObject.layer == GROUND) PlayerWantsToMove?.Invoke(hit.point);
            else if (hit.collider.gameObject.layer == SKILL) PlayerWantsToSkill?.Invoke(hit.collider.gameObject, hit.point);
        }
    }
}
