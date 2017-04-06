using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEntity : BaseEntity {

    Vector3[] path;
    protected Vector3 currentWaypoint;
    int targetIndex;

    // this is really ugly, but I don't know a better solution right now
    bool wanderSuccess = true;

    public float Speed { get { return 1; } }
    [Range(2,15)]
    public float WanderDistance;

    public void RequestPathToTarget(Transform target) {
        PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
    }
    public void RequestPathToTarget(Vector3 target) {
        PathRequestManager.RequestPath(transform.position, target, OnPathFound);
    }

    public void OnPathFound(Vector3[] newPath, bool pathSuccessful) {
        if (pathSuccessful) {
            path = newPath;
            targetIndex = 0;
            try {
                currentWaypoint = path[0];
            } catch(Exception e) {
                currentWaypoint = transform.position;
            }
            wanderSuccess = true;
        }
        else {
            wanderSuccess = false;
            //Status = ActionEnum.STATUS_FAILED;
        }
    }

    public ActionEnum ExecuteFollowPath() {
        if (path != null) {
            if (transform.position == currentWaypoint) {
                targetIndex++;
                if (targetIndex >= path.Length) {
                    //completed
                    //Destroy(this.gameObject);
                    return ActionEnum.STATUS_COMPLETED;
                }
                currentWaypoint = path[targetIndex];
            }

            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, Speed * Time.deltaTime);
        }
        // not yet completed
        return ActionEnum.STATUS_ACTIVE;
    }

    public Vector3 GetRandomWanderTarget() {   
        float randomX = UnityEngine.Random.Range(-WanderDistance, WanderDistance);
        float randomZ = UnityEngine.Random.Range(-WanderDistance, WanderDistance);

        Vector3 targetPosition =  new Vector3(
            transform.position.x + randomX, 
            transform.position.y, 
            transform.position.z + randomZ
        );
        return targetPosition;
        //RequestPathToTarget(targetPosition);
    }

    public ActionEnum WanderStatus() {
        return wanderSuccess ? ActionEnum.STATUS_ACTIVE : ActionEnum.STATUS_FAILED;
    }

    public void OnDrawGizmos() {
        if (path != null) {
            for (int i = targetIndex; i < path.Length; i++) {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(path[i], Vector3.one);

                if (i == targetIndex) {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }
}
