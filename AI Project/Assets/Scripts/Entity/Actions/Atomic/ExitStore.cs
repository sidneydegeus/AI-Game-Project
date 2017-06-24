using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ExitStore : Action {

    Renderer render;
    float fadeOut;

    public ExitStore(BaseEntity _entity) : base(_entity) {
        Description = "Exit store (A)";
        render = entity.transform.GetChild(0).GetComponent<Renderer>();
        fadeOut = 0.00f;
    }

    public override void Activate() {
        Status = ActionEnum.STATUS_ACTIVE;
    }

    public override ActionEnum Process() {
        fadeOut += 0.01f;
        if (fadeOut >= 1.00f) {
            render.enabled = true;
            Status = ActionEnum.STATUS_COMPLETED;
            Debug.Log("exiting store");
        }
        return Status;
    }

    public override void Terminate() {
        //throw new NotImplementedException();
    }
}
