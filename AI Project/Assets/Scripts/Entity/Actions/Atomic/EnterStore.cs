using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class EnterStore : Action {

    Renderer render;
    float fadeOut;

    public EnterStore(Human _entity) : base(_entity) {
        Description = "Enter store (A)";
        render = entity.transform.GetChild(0).GetComponent<Renderer>();
        fadeOut = 1.00f;
    }

    public override void Activate() {
        Status = ActionEnum.STATUS_ACTIVE;
    }

    public override ActionEnum Process() {
        fadeOut -= 0.01f;
        if (fadeOut <= 0) {
            render.enabled = false;
            Status = ActionEnum.STATUS_COMPLETED;
            Debug.Log("entering store");
        }
        return Status;
    }

    public override void Terminate() {
        //throw new NotImplementedException();
    }
}
