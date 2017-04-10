
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

partial class MovingEntity {
    public class SeekBehaviour : ISteeringBehaviour {

        MovingEntity entity;
        WanderBehaviour wanderBehaviour;

        public SeekBehaviour(MovingEntity _entity) {
            entity = _entity;
            wanderBehaviour = new WanderBehaviour(entity);
        }

        public ActionEnum Execute() {
            ActionEnum status = wanderBehaviour.Execute();
            if (status == ActionEnum.STATUS_COMPLETED && entity.visibleTargets.Count <= 0) {
                wanderBehaviour = new WanderBehaviour(entity);
                status = ActionEnum.STATUS_ACTIVE;
            } else if (entity.visibleTargets.Count > 0){
                int randomTarget = Random.Range(0, entity.visibleTargets.Count);
                entity.LockedTarget = entity.visibleTargets[randomTarget];
                status = ActionEnum.STATUS_COMPLETED;
            }

            return status;

        }
    }
}
