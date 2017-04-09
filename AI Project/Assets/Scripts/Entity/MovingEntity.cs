using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEntity : BaseEntity {

    const float minPathUpdateTime = .2f;
    const float pathUpdateMoveThreshold = .5f;

    public Transform target;
    public float turnSpeed = 3;
    public float turnDst = 3;
    public float stoppingDst = 10;

    float speedPercent = 1;
    Path path;
    bool wanderSuccess = true;

    [HideInInspector]
    public bool DisplayPathfindToggle;

    public float Speed { get { return 3; } }
    [Range(2, 15)]
    public float WanderDistance;

    public void RequestPathToTarget(Transform target) {
        PathRequestManager.RequestPath(new PathRequest(transform.position, target.position, OnPathFound));
    }
    public void RequestPathToTarget(Vector3 target) {
        PathRequestManager.RequestPath(new PathRequest(transform.position, target, OnPathFound));
    }

    void Start()
    {
        StartCoroutine(UpdatePath());
    }

    public void OnPathFound(Vector3[] waypoints, bool pathSuccessful) {
        if (pathSuccessful) {
            path = new Path(waypoints, transform.position, turnDst, stoppingDst);
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
        PathRequestManager.RequestPath(new PathRequest(transform.position, target.position, OnPathFound));

        float sqrMoveThreshold = pathUpdateMoveThreshold * pathUpdateMoveThreshold;
        Vector3 targetPosOld = target.position;

        while (true)
        {
            yield return new WaitForSeconds(minPathUpdateTime);
            if ((target.position - targetPosOld).sqrMagnitude > sqrMoveThreshold)
            {
                PathRequestManager.RequestPath(new PathRequest(transform.position, target.position, OnPathFound));
                targetPosOld = target.position;
            }
        }
    }

    public ActionEnum ExecuteFollowPath() {
        bool followingPath = true;
        int pathIndex = 0;
        //    transform.LookAt(path.lookPoints[0]);
        if (path != null) {
            Vector2 pos2D = new Vector2(transform.position.x, transform.position.z);
            while (path.turnBoundaries[pathIndex].HasCrossedLine(pos2D))
            {
                if (pathIndex == path.finishLineIndex)
                {
                    followingPath = false;
                    return ActionEnum.STATUS_COMPLETED;
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
                        return ActionEnum.STATUS_COMPLETED;
                    }
                }

                Quaternion targetRotation = Quaternion.LookRotation(path.lookPoints[pathIndex] - transform.position);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
                transform.Translate(Vector3.forward * Time.deltaTime * Speed * speedPercent, Space.Self);
            }
        }
        return ActionEnum.STATUS_ACTIVE;
    }

    public Vector3 GetRandomWanderTarget() {
        float randomX = UnityEngine.Random.Range(-WanderDistance, WanderDistance);
        float randomZ = UnityEngine.Random.Range(-WanderDistance, WanderDistance);

        Vector3 targetPosition = new Vector3(
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
        if (path != null)
        {
            path.DrawWithGizmos();
        }
    }
}