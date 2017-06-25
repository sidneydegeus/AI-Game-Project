using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract  class MovingEntity : BaseEntity {

    const float minPathUpdateTime = .2f;
    const float pathUpdateMoveThreshold = .5f;

    // make a target position too?

    public float turnSpeed = 3;
    public float turnDst = 3;
    public float stoppingDst = 10;
    public LineRenderer lineRenderer;
   
    public GameObject waypointPrefab;

    // internal so that behaviours can make use of it
    internal Path path;
    internal bool wanderSuccess = true;
    internal float speedPercent = 1;


    [HideInInspector]
    public bool DisplayPathfindToggle;

    [Range(1,2)]
    public float Speed;
    [Range(10, 15)]
    public float WanderRadius = 25;

    List<GameObject> waypointBlocks = new List<GameObject>();

    protected override void Start() {
        base.Start();
    }

    protected override void Update() {
        base.Update();
    }

    public ActionEnum WanderStatus() {
        return wanderSuccess ? ActionEnum.STATUS_ACTIVE : ActionEnum.STATUS_FAILED;
    }

    protected void DisplayWaypoints() {
        foreach (GameObject go in waypointBlocks) {
            Destroy(go);
        }
        GameObject prevWaypoint = Instantiate(waypointPrefab, path.lookPoints[0], Quaternion.identity) as GameObject;
        waypointBlocks.Add(prevWaypoint);
        for (int i = 1; i < path.lookPoints.Length; i++) {
            GameObject lineRend = Instantiate(waypointPrefab, path.lookPoints[i], Quaternion.identity) as GameObject;
            GameObject wayp = Instantiate(waypointPrefab, path.lookPoints[i], Quaternion.identity) as GameObject;

            waypointBlocks.Add(wayp);
            Destroy(prevWaypoint);
            prevWaypoint = wayp;
        } 
    }

    public void OnDrawGizmos() {
        if (path != null)
        {
            path.DrawWithGizmos();
        }
    }
}