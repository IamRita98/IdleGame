using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] int speed = 10;
    [SerializeField] float minimumDistance;
    private void OnEnable()
    {
        MouseRayCastManager.PlayerWantsToMove += MovePlayer;
    }
    private void OnDisable()
    {
        MouseRayCastManager.PlayerWantsToMove -= MovePlayer;
    }

    void MovePlayer(Vector3 targetPos)
    {
        
        print("Moving to " + targetPos);
        while(gameObject.transform.position > Vector3.Distance)
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPos, speed);
    }
}
