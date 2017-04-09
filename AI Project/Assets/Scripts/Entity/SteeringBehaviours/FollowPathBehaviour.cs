using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

partial class MovingEntity {
    public class FollowPathBehaviour : ISteeringBehaviour {

        MovingEntity entity;

        public FollowPathBehaviour(MovingEntity _entity, Vector3 target) {
            entity = _entity;
            RequestPathToTarget(target);
        }

        void RequestPathToTarget(Transform target) {
            PathRequestManager.RequestPath(new PathRequest(entity.transform.position, target.position, OnPathFound));
        }
        void RequestPathToTarget(Vector3 target) {
            PathRequestManager.RequestPath(new PathRequest(entity.transform.position, target, OnPathFound));
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

        public ActionEnum Execute() {
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
 

    }
}

