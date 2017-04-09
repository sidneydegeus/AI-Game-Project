using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MovingEntity : BaseEntity {

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

    [Range(1,2)]
    public float Speed;
    [Range(2, 15)]
    public float WanderDistance;



    //protected IEnumerator UpdatePath()
    //{
    //    if (Time.timeSinceLevelLoad < .3f)
    //    {
    //        yield return new WaitForSeconds(.3f);
    //    }
    //    PathRequestManager.RequestPath(new PathRequest(transform.position, target.position, OnPathFound));

    //    float sqrMoveThreshold = pathUpdateMoveThreshold * pathUpdateMoveThreshold;
    //    Vector3 targetPosOld = target.position;

    //    while (true)
    //    {
    //        yield return new WaitForSeconds(minPathUpdateTime);
    //        if ((target.position - targetPosOld).sqrMagnitude > sqrMoveThreshold)
    //        {
    //            PathRequestManager.RequestPath(new PathRequest(transform.position, target.position, OnPathFound));
    //            targetPosOld = target.position;
    //        }
    //    }
    //}



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