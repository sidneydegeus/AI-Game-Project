using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewHuman : MovingEntity {

	protected override void Start () {
        base.Start();

        thinkBehaviour = new HumanThinkBehaviour(this);
        entityBehaviours[BehaviourEnum.WANDER_BEHAVIOUR] = new WanderBehaviour(this);
        entityBehaviours[BehaviourEnum.FOLLOW_BEHAVIOUR] = new FollowpathBehaviour(this);
    }

    // Update is called once per frame
    protected override void Update () {
        base.Update();
	}
}
