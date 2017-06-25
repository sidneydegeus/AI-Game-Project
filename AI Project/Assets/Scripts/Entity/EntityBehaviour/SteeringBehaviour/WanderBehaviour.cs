using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.AI;

public class WanderBehaviour : SteeringBehaviour {

    Vector3 newPos;

    public WanderBehaviour(MovingEntity _entity) : base(_entity) {
    }

    public override void Init() {
        entity.TargetPosition = GetRandomWanderTarget();
        entity.entityBehaviours[BehaviourEnum.FOLLOW_BEHAVIOUR].Init();
    }

    public override ActionEnum Process() {
        return entity.entityBehaviours[BehaviourEnum.FOLLOW_BEHAVIOUR].Process();
    }

    Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask) {
        Vector3 randDirection = UnityEngine.Random.insideUnitSphere * dist;
        randDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);
        return navHit.position;
    }

    Vector3 GetRandomWanderTarget() {
        float randomX = UnityEngine.Random.Range(-entity.WanderRadius, entity.WanderRadius);
        float randomZ = UnityEngine.Random.Range(-entity.WanderRadius, entity.WanderRadius);

        Vector3 targetPosition = new Vector3(
            entity.transform.position.x + randomX,
            entity.transform.position.y,
            entity.transform.position.z + randomZ
        );

        while (targetPosition.x > 15.5 || targetPosition.x < -15.5)
        {
            randomX = UnityEngine.Random.Range(-entity.WanderRadius, entity.WanderRadius);
            targetPosition.x = entity.transform.position.x + randomX;
        }

        while (targetPosition.z > 15.5 || targetPosition.z < -15.5)
        {
            randomZ = UnityEngine.Random.Range(-entity.WanderRadius, entity.WanderRadius);
            targetPosition.z = entity.transform.position.z + randomZ;
        }

        return targetPosition;
    }
}