using System;
using UnityEngine;

public abstract class Action : MonoBehaviour {

    public string Description { get; protected set; }
    public ActionEnum Status { get; set; }
    public int Weight { get; protected set; }
    protected MovingEntity entity;

 
    // add a weight variable later?

    public abstract void Activate();
    public abstract ActionEnum Process();
    public abstract void Terminate();

    public Action(MovingEntity _entity) {
        entity = _entity;
        // upon creation, the action is initially inactive.
        Status = ActionEnum.STATUS_INACTIVE;
    }
}

