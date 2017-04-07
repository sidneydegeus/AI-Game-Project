using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEntity : BaseEntity {

    const float minPathUpdateTime = .2f;
    const float pathUpdateMoveThreshold = .5f;
    protected Vector3 currentWaypoint;
    Path path;

    public Transform target;
    public float turnSpeed = 3;
    public float turnDst = 5;
    public float stoppingDst = 10;

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

  //  void Start()
  //  {
  //      StartCoroutine(UpdatePath());
   // }

    public void OnPathFound(Vector3[] waypoints, bool pathSuccessful) {
        if (pathSuccessful) {
            path = new Path(waypoints, transform.position, turnDst, stoppingDst);
            try {
                currentWaypoint = waypoints[0];
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

    IEnumerator UpdatePath()
    {
        if (Time.timeSinceLevelLoad < .3f)
        {
            yield return new WaitForSeconds(.3f);
        }
        PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);

        float sqrMoveThreshold = pathUpdateMoveThreshold * pathUpdateMoveThreshold;
        Vector3 targetPosOld = target.position;

        while (true)
        {
            yield return new WaitForSeconds(minPathUpdateTime);
            if ((target.position - targetPosOld).sqrMagnitude > sqrMoveThreshold)
            {
                PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
                targetPosOld = target.position;
            }
        }
    }

    public ActionEnum ExecuteFollowPath() {
        if (path != null) {
            bool followingPath = true;
            int pathIndex = 0;
            transform.LookAt(path.lookPoints[0]);
            float speedPercent = 1.5f;

          //  while (followingPath)
        //    {
                Vector2 pos2D = new Vector2(transform.position.x, transform.position.z);
                while (path.turnBoundaries[pathIndex].HasCrossedLine(pos2D))
                {
                    if (pathIndex == path.finishLineIndex)
                    {
                        followingPath = false;
                        break;
                    }
                    else
                    {
                        pathIndex++;
                    }
                }

                if (followingPath)
                {

                    if (pathIndex >= path.slowDownIndex && stoppingDst > 0)
                    {
                        speedPercent = Mathf.Clamp01(path.turnBoundaries[path.finishLineIndex].DistanceFromPoint(pos2D) / stoppingDst);
                        if (speedPercent < 0.01f)
                        {
                            followingPath = false;
                        }
                }
                    Quaternion targetRotation = Quaternion.LookRotation(path.lookPoints[pathIndex] - transform.position);
                    transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
                    transform.Translate(Vector3.forward * Time.deltaTime * Speed * speedPercent, Space.Self);
                }

        //    }

            //if (transform.position == currentWaypoint) {
            //    targetIndex++;
            //    if (targetIndex >= path.Length) {
            //        //completed
            //        //Destroy(this.gameObject);
            //        return ActionEnum.STATUS_COMPLETED;
            //    }
            //    currentWaypoint = path[targetIndex];
            //}

            //transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, Speed * Time.deltaTime);
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
            path.DrawWithGizmos();
        }
    }
}
