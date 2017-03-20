using Assets.Scripts.Unit;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

class FollowPath : Action {

    Vector3[] path;
    Vector3 currentWaypoint;
    int targetIndex;
    Transform target;

    public FollowPath(MonoBehaviour _unit, Transform _target) : base(_unit) { // add a target as well? so multiple targets can be made
        Description = "Following path to target";
        target = _target;
        //target = unit.GetComponent<Unit>().target;
    }

    public override void Activate() {
        Status = ActionEnum.STATUS_ACTIVE;
        //Debug.Log("im getting activated");
        PathRequestManager.RequestPath(unit.transform.position, target.position, OnPathFound);
    }

    public override ActionEnum Process() {
        ExecuteFollowPath();
        return Status;
    }

    public override void Terminate() {
        throw new NotImplementedException();
    }

    public void OnPathFound(Vector3[] newPath, bool pathSuccessful) {
        if (pathSuccessful) {
            path = newPath;
            targetIndex = 0;
            currentWaypoint = path[0];
        } else {
            Status = ActionEnum.STATUS_FAILED;
        }
    }

    void ExecuteFollowPath() {
        if (path != null) {
            if (unit.transform.position == currentWaypoint) {
                targetIndex++;
                if (targetIndex >= path.Length) {
                    Status = ActionEnum.STATUS_COMPLETED;
                    return;
                }
                currentWaypoint = path[targetIndex];
            }
            unit.transform.position = Vector3.MoveTowards(unit.transform.position, currentWaypoint, unit.GetComponent<Unit>().Speed * Time.deltaTime);
        }
    }

    //IEnumerator ExecuteFollowPath() {
    //    Vector3 currentWaypoint = path[0];
    //    while (true) {
    //        if (unit.transform.position == currentWaypoint) {
    //            targetIndex++;
    //            if (targetIndex >= path.Length) {
    //                Status = ActionEnum.STATUS_COMPLETED;
    //                yield break;
    //            }
    //            currentWaypoint = path[targetIndex];
    //        }
    //        unit.transform.position = Vector3.MoveTowards(unit.transform.position, currentWaypoint, unit.GetComponent<Unit>().Speed * Time.deltaTime);
    //        yield return null;

    //    }
    //}

    public void OnDrawGizmos() {
        if (path != null) {
            for (int i = targetIndex; i < path.Length; i++) {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(path[i], Vector3.one);

                if (i == targetIndex) {
                    Gizmos.DrawLine(unit.transform.position, path[i]);
                }
                else {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }
}

