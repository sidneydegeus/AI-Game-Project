using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

partial class MovingEntity {
    public class WanderBehaviour : ISteeringBehaviour {

        MovingEntity entity;
        FollowPathBehaviour followPathBehaviour;

        public WanderBehaviour(MovingEntity _entity) {
            entity = _entity;
            followPathBehaviour = new FollowPathBehaviour(entity, GetRandomWanderTarget());
        }

        public ActionEnum Execute() {
            return followPathBehaviour.Execute();
        }

        Vector3 GetRandomWanderTarget() {
            float randomX = UnityEngine.Random.Range(-entity.WanderDistance, entity.WanderDistance);
            float randomZ = UnityEngine.Random.Range(-entity.WanderDistance, entity.WanderDistance);

            Vector3 targetPosition = new Vector3(
                entity.transform.position.x + randomX,
                entity.transform.position.y,
                entity.transform.position.z + randomZ
            );
            return targetPosition;
        }
    }
}

