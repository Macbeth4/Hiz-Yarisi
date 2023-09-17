using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class dusmanarabazeka : MonoBehaviour
{

    public enum AIMode { followPlayer, followWaypoints };

    [Header("AI ayarları")]
    public AIMode aiMode;

    Vector3 targetPosition = Vector3.zero;
    Transform targetTransform = null;

    WaypointNode currentWaypoint = null;
    WaypointNode[] allWayPoints;

    araba araba;

    private void Awake() {
        araba = GetComponent<araba>();
        allWayPoints = FindObjectsOfType<WaypointNode>();
    }

    private void FixedUpdate() {
        if(GameManager.instance.GetGameStates()==GameStates.countdown){return;}

        Vector2 inputVector = Vector2.zero;

        switch(aiMode){
            case AIMode.followPlayer:{
                FollowPlayer();
                break;
            }
            case AIMode.followWaypoints:{
                FollowWaypoints();
                break;
            }
        }

        inputVector.x = TurnTowardTarget();
        inputVector.y = 1.0f;

    

        araba.SetInputVector(inputVector);
    }

    void FollowWaypoints(){
        if(currentWaypoint == null){currentWaypoint = FindClosestWayPoint();}

        if(currentWaypoint != null)
        {
            targetPosition = currentWaypoint.transform.position;
            float distanceToWayPoint = (targetPosition - transform.position).magnitude;

            if(distanceToWayPoint <= currentWaypoint.minDistanceToReachWaypoint){
                currentWaypoint = currentWaypoint.nextWaypointNode[Random.Range(0,currentWaypoint.nextWaypointNode.Length)];
            }
        }
    }

    WaypointNode FindClosestWayPoint(){
        return allWayPoints
        .OrderBy(t => Vector3.Distance(transform.position, t.transform.position))
        .FirstOrDefault();
    }

    void FollowPlayer(){
        if(targetTransform == null)
        {
            targetTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }

        if(targetTransform != null){targetPosition = targetTransform.position;}

    }

    float TurnTowardTarget(){
        Vector2 vectorToTarget = targetPosition - transform.position;
        vectorToTarget.Normalize();

        float angleToTarget = Vector2.SignedAngle(transform.up,vectorToTarget);
        angleToTarget *= -1;

        float steerAmount = angleToTarget / 45.0f;

        steerAmount = Mathf.Clamp(steerAmount,-1.0f,1.0f); 
        return steerAmount;
    }

}