using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.AI;

public class WanderBehaviour : SteeringBehaviour {

    Vector3 newPos;

    public WanderBehaviour(MovingEntity _entity) : base(_entity) {
        //followpathBehaviour = new FollowPathBehaviour(entity, GetRandomWanderTarget());
    }

    public override void Init() {
        entity.TargetPosition = GetRandomWanderTarget();
        //newPos = RandomNavSphere(entity.transform.position, entity.WanderRadius, -1);
        entity.entityBehaviours[BehaviourEnum.FOLLOW_BEHAVIOUR].Init();
    }

    public override ActionEnum Process() {
        
        //entity.NavAgent.SetDestination(newPos);
        //if (!entity.NavAgent.pathPending) {
        //    if (entity.NavAgent.remainingDistance <= entity.NavAgent.stoppingDistance) {
        //        if (!entity.NavAgent.hasPath || entity.NavAgent.velocity.sqrMagnitude == 0f) {
        //            entity.animator.SetBool("Moving", false);
        //            return ActionEnum.STATUS_COMPLETED;
        //        }
        //    }
        //}
        //// inaccessable path
        //if (entity.NavAgent.pathStatus == NavMeshPathStatus.PathPartial) {
        //    return ActionEnum.STATUS_FAILED;
        //}

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
        return targetPosition;
    }
}

//public class WanderBehaviour : SteeringBehaviour {

//    MovingEntity entity;
//    FollowPathBehaviour followPathBehaviour;

//    public WanderBehaviour(MovingEntity _entity) {
//        entity = _entity;
//        followPathBehaviour = new FollowPathBehaviour(entity, GetRandomWanderTarget());
//    }

//    public ActionEnum Execute() {
//        return followPathBehaviour.Execute();
//    }

//public override void Init() {
//    throw new NotImplementedException();
//}

//public override ActionEnum Process() {
//    return followPathBehaviour.Process();
//}




