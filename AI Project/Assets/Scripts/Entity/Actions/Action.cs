using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action : MonoBehaviour {

    public string Description { get; protected set; }
    public ActionEnum Status { get; set; }
    public int Weight { get; protected set; }

    protected Human entity;
    protected LinkedList<Action> actionLinkedList;

    // add a weight variable later?

    public abstract void Activate();
    public abstract ActionEnum Process();
    public abstract void Terminate();

    public Action(Human _entity) {
        entity = _entity;
        Status = ActionEnum.STATUS_INACTIVE;
        actionLinkedList = new LinkedList<Action>();
    }

    public void GetDescription() {
        entity.ActionDescriptionList.Add(Description);
        foreach (Action action in actionLinkedList) {
            action.GetDescription();
        }
    }

    // public only so that custom movement path can be chosen...
    public void AddAction(Action action) {
        actionLinkedList.AddFirst(action);
    }

    protected Action CurrentAction() {
        return actionLinkedList.First.Value;
    }

    protected Action NextAction() {
        return actionLinkedList.First.Next.Value;
    }

    protected void RemoveAction() {
        actionLinkedList.RemoveFirst();
    }

    protected void AddActions(List<Action> actions) {
        actions.Reverse();
        foreach (Action action in actions) {
            AddAction(action);
        }
    }

    protected int ActionListCount() {
        return actionLinkedList.Count;
    }
}

