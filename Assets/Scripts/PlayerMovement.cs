using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum PlayerState
{
    idle,
    walking,
    skilling,
    combat
}

public class PlayerMovement : MonoBehaviour
{
    SkillingManager skillingManager;
    [SerializeField] int speed = 10;
    float minimumDistance = 4f;
    Vector3 targetPos;
    Rigidbody rb;
    bool checkForArrival = false;
    GameObject skillToWalkTowards;
    float extraSpaceLeftBetweenSkillAndPlayer = .5f;

    public PlayerState currentState;

    private void Awake()
    {
        skillingManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<SkillingManager>();
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        MouseRayCastManager.PlayerWantsToMove += MovePlayer;
        MouseRayCastManager.PlayerWantsToSkill += MoveToSkill;
    }
    private void OnDisable()
    {
        MouseRayCastManager.PlayerWantsToMove -= MovePlayer;
        MouseRayCastManager.PlayerWantsToSkill -= MoveToSkill;
    }

    private void Update()
    {
        if (rb.velocity == Vector3.zero) currentState = PlayerState.idle;
        if (!checkForArrival) return;
        if (Vector3.Distance(transform.position, skillToWalkTowards.transform.position) <= minimumDistance + extraSpaceLeftBetweenSkillAndPlayer)
        {
            StopPlayer();
            currentState = PlayerState.skilling;
            skillingManager.CheckSkillToStart(skillToWalkTowards);
        }
    }

    void MovePlayer(Vector3 targetPosition)
    {
        currentState = PlayerState.walking;
        checkForArrival = false;
        targetPos = targetPosition;
        Vector3 dir = targetPos - transform.position;
        print("Moving to " + targetPos);
        rb.velocity = dir.normalized * speed;
    }

    void StopPlayer()
    {
        checkForArrival = false;
        rb.velocity = Vector3.zero;
    }

    void MoveToSkill(GameObject goClicked, Vector3 clickPos)
    {
        skillToWalkTowards = goClicked;
        if (Vector3.Distance(transform.position, goClicked.transform.position) > minimumDistance)
        {
            MovePlayer(clickPos);
            checkForArrival = true;
        }
    }
}
