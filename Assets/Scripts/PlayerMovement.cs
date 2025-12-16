using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] int speed = 10;
    [SerializeField] float minimumDistance = .05f;
    Vector3 targetPos;
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        MouseRayCastManager.PlayerWantsToMove += MovePlayer;
    }
    private void OnDisable()
    {
        MouseRayCastManager.PlayerWantsToMove -= MovePlayer;
    }

    void MovePlayer(Vector3 targetPosition)
    {
        targetPos = targetPosition;
        Vector3 dir = targetPos - transform.position;
        print("Moving to " + targetPos);
        rb.velocity = dir.normalized * speed;
    }
}
