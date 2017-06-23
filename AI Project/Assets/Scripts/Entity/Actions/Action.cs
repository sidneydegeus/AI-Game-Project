﻿using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action {

    public string Description { get; protected set; }
    public ActionEnum Status { get; set; }

    protected BaseEntity entity;
    protected LinkedList<Action> actionLinkedList;

    public abstract void Activate();
    public abstract ActionEnum Process();
    public virtual void Terminate() {
        Status = ActionEnum.STATUS_CANCELED;
    }

    #region Action Constructors

    public Action() {
        Status = ActionEnum.STATUS_INACTIVE;
        actionLinkedList = new LinkedList<Action>();
    }

    public Action(BaseEntity _entity) {
        entity = _entity;
        Status = ActionEnum.STATUS_INACTIVE;
        actionLinkedList = new LinkedList<Action>();
    }

#endregion

    public void SetEntity(BaseEntity _entity) {
        entity = _entity;
    }

    public void GetDescription() {
        entity.ActionDescriptionList.Add(Description);
        foreach (Action action in actionLinkedList) {
            action.GetDescription();
        }
    }

    public void AddAction(Action action) {
        actionLinkedList.AddFirst(action);
    }

    public Action CurrentAction() {
        return actionLinkedList.First.Value;
    }

    public Action NextAction() {
        return actionLinkedList.First.Next.Value;
    }

    public void RemoveAction() {
        actionLinkedList.RemoveFirst();
    }

    public void AddActions(List<Action> actions) {
        //actions.Reverse();
        foreach (Action action in actions) {
            AddAction(action);
        }
    }

    public int ActionListCount() {
        return actionLinkedList.Count;
    }
}