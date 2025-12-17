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
    float minimumDistance = 1.5f;
    Vector3 targetPos;
    Rigidbody rb;
    public bool checkForArrival = false;
    GameObject skillToWalkTowards;
    float extraSpaceLeftBetweenSkillAndPlayer = 2.5f;
    Vector3 dir;

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
        if (!checkForArrival) return;
        currentState = PlayerState.walking;
        transform.position = Vector3.Lerp(transform.position, targetPos, speed * Time.deltaTime); //Need new way to handle movement
        if(skillToWalkTowards != null)
        {
            if (Vector3.Distance(transform.position, skillToWalkTowards.transform.position) <= minimumDistance + extraSpaceLeftBetweenSkillAndPlayer)
            {
                currentState = PlayerState.skilling;
                skillingManager.CheckSkillToStart(skillToWalkTowards);
                StopPlayer();
            }
        }
        else if (Vector3.Distance(transform.position, targetPos) <= minimumDistance) StopPlayer();
    }

    void MovePlayer(Vector3 targetPosition)
    {
        currentState = PlayerState.walking;
        checkForArrival = true;
        targetPos = targetPosition;
        dir = targetPos - transform.position;
        print("Moving to " + targetPos);
    }

    void StopPlayer()
    {
        if (currentState != PlayerState.skilling) currentState = PlayerState.idle;
        skillToWalkTowards = null;
        checkForArrival = false;
        rb.velocity = Vector3.zero;
    }

    void MoveToSkill(GameObject goClicked, Vector3 clickPos)
    {
        skillToWalkTowards = goClicked;
        if (Vector3.Distance(transform.position, goClicked.transform.position) > minimumDistance) MovePlayer(clickPos);
    }
}
