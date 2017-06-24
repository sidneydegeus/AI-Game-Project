using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class FollowpathBehaviour : SteeringBehaviour {

    public FollowpathBehaviour(MovingEntity _entity) : base(_entity) {
        entity = _entity;
    }

    #region SteeringBehaviour Implementation Init() / Process()

    public override void Init() {
        RequestPathToTarget((entity.Target == null) ? entity.TargetPosition : entity.Target.position);
    }

    public override ActionEnum Process() {
        //lineOnScreen();

        bool followingPath = true;
        int pathIndex = 0;
        //    transform.LookAt(path.lookPoints[0]);
        if (entity.path != null) {
            Vector2 pos2D = new Vector2(entity.transform.position.x, entity.transform.position.z);
            while (entity.path.turnBoundaries[pathIndex].HasCrossedLine(pos2D)) {
                if (pathIndex == entity.path.finishLineIndex) {
                    followingPath = false;
                    return ActionEnum.STATUS_COMPLETED;
                }
                else {
                    pathIndex++;
                }
            }

            if (followingPath) {
                if (pathIndex >= entity.path.slowDownIndex && entity.stoppingDst > 0) {
                    entity.speedPercent = Mathf.Clamp01(entity.path.turnBoundaries[entity.path.finishLineIndex].DistanceFromPoint(pos2D) / entity.stoppingDst);
                    if (entity.speedPercent < 0.01f) {
                        followingPath = false;
                        return ActionEnum.STATUS_COMPLETED;
                    }
                }

                Quaternion targetRotation = Quaternion.LookRotation(entity.path.lookPoints[pathIndex] - entity.transform.position);
                entity.transform.rotation = Quaternion.Lerp(entity.transform.rotation, targetRotation, Time.deltaTime * entity.turnSpeed);
                entity.transform.Translate(Vector3.forward * Time.deltaTime * entity.Speed * entity.speedPercent, Space.Self);
            }
        }
        return ActionEnum.STATUS_ACTIVE;
    }

    #endregion

    void RequestPathToTarget(Transform target) {
        PathRequestManager.RequestPath(new PathRequest(entity.transform.position, target.position, OnPathFound));
    }
    void RequestPathToTarget(Vector3 target) {
        PathRequestManager.RequestPath(new PathRequest(entity.transform.position, target, OnPathFound));
        Debug.Log("Target: " + target);
        lineOnScreen(target);
    }

    public void OnPathFound(Vector3[] waypoints, bool pathSuccessful) {
        if (pathSuccessful) {
            entity.path = new Path(waypoints, entity.transform.position, entity.turnDst, entity.stoppingDst);
            entity.wanderSuccess = true;
        }
        else {
            entity.wanderSuccess = false;
            //Status = ActionEnum.STATUS_FAILED;
        }
    }

    public void lineOnScreen(Vector3 target) {
        Material whiteDiffuseMat = new Material(Shader.Find("Unlit/Texture"));
        entity.lineRenderer.material = whiteDiffuseMat;
        entity.lineRenderer.SetPosition(0, entity.transform.position);
        entity.lineRenderer.SetPosition(1, target);
    }

}


