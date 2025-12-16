using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] int speed = 10;
    [SerializeField] float minimumDistance = 10;
    bool movePlayer = false;
    Vector3 targetPos;
    private void OnEnable()
    {
        MouseRayCastManager.PlayerWantsToMove += MovePlayer;
    }
    private void OnDisable()
    {
        MouseRayCastManager.PlayerWantsToMove -= MovePlayer;
    }

    private void Update()
    {
        if(movePlayer == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed);
        }
    }

    void MovePlayer(Vector3 targetPosition)
    {
        targetPos = targetPosition;
        print("Moving to " + targetPos);
        movePlayer = true;
    }
}
