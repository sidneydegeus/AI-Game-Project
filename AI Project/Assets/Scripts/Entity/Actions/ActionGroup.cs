using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionGroup : Action {
    
    public ActionGroup(Human _entity) : base(_entity) {}

    override
    public void Activate() {
        Status = ActionEnum.STATUS_ACTIVE;
    }

    override
    public void Terminate() {
        actionLinkedList.Clear();
    }

    override
    public ActionEnum Process() {
        AdditionalProcess();

        if (ActionListCount() > 0) {
            Action action = CurrentAction();
            if (action.Status == ActionEnum.STATUS_INACTIVE) {
                action.Activate();
            }
            action.Process();
            if (action.Status == ActionEnum.STATUS_COMPLETED || action.Status == ActionEnum.STATUS_FAILED) {
                RemoveAction();
            }
        } else {
            this.Status = ActionEnum.STATUS_COMPLETED;
        }

        if (Status == ActionEnum.STATUS_FAILED) {
            Debug.Log("do i do this?");
            Terminate();
            return Status;
        }
        return Status;
    }

    protected abstract void AdditionalProcess();
}
