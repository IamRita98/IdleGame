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
    IdleSkilling idleSkilling;
    [SerializeField] int speed = 10;
    float minimumDistance = 1.5f;
    Vector3 targetPos;
    Rigidbody rb;
    public bool checkForArrival = false;
    GameObject skillToWalkTowards;
    float extraSpaceLeftBetweenSkillAndPlayer = 2.5f;
    Vector3 dir;
    PlayerItemManager playerItemManager;

    public PlayerState currentState;

    private void Awake()
    {
        idleSkilling = GetComponent<IdleSkilling>();
        rb = GetComponent<Rigidbody>();
        playerItemManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerItemManager>();
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
        Controls();
        if (!checkForArrival) return;
        currentState = PlayerState.walking;
        transform.position = Vector3.Lerp(transform.position, targetPos, speed * Time.deltaTime); //Need new way to handle movement
        if(skillToWalkTowards != null)
        {
            if (Vector3.Distance(transform.position, skillToWalkTowards.transform.position) <= minimumDistance + extraSpaceLeftBetweenSkillAndPlayer)
            {
                idleSkilling.DetermineSkillingType(skillToWalkTowards);
                StopPlayer();
            }
        }
        else if (Vector3.Distance(transform.position, targetPos) <= minimumDistance) StopPlayer();
    }

    void Controls()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ShowInventory();
        }
    }

    void ShowInventory()
    {
        foreach (Item item in playerItemManager.inventory)
        {
            print(item.itemName);
        }
    }

    void MovePlayer(Vector3 targetPosition)
    {
        currentState = PlayerState.walking;
        checkForArrival = true;
        targetPos = targetPosition;
        dir = targetPos - transform.position;
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
