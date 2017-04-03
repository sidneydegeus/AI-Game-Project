using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEntity : LivingEntity {

    Vector3[] path;
    Vector3 currentWaypoint;
    int targetIndex;

    public float Speed { get { return 1; } }

    public void RequestPathToTarget(Transform target) {
        PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
    }

    public void OnPathFound(Vector3[] newPath, bool pathSuccessful) {
        if (pathSuccessful) {
            path = newPath;
            targetIndex = 0;
            currentWaypoint = path[0];
        }
        else {
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
