using Assets.Scripts.Unit;
using System;
using UnityEngine;

public abstract class Action : MonoBehaviour {

    public string Description { get; protected set; }
    public ActionEnum Status { get; set; }
    public int Weight { get; protected set; }
    protected MovingEntity unit;

 
    // add a weight variable later?

    public abstract void Activate();
    public abstract ActionEnum Process();
    public abstract void Terminate();

    public Action(MovingEntity _unit) {
        unit = _unit;
        // upon creation, the action is initially inactive.
        Status = ActionEnum.STATUS_INACTIVE;
    }
}

