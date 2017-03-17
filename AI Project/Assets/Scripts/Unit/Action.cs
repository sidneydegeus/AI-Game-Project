using Assets.Scripts.Unit;
using System;

public abstract class Action {

    public string Description { get; protected set; }

    ActionEnum Status { get; set; }

    public abstract void Activate();
    public abstract int Process();
    public abstract void Terminate();
}

