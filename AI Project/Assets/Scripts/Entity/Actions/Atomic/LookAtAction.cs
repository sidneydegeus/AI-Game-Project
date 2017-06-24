using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtAction : Action {

    // in all atomics, make use of strategy pattern
    public LookAtAction() : base() { }
    public LookAtAction(BaseEntity _entity) : base(_entity) {
        Description = "LookAt (A)";
    }

    public override void Activate() {
        Status = ActionEnum.STATUS_ACTIVE;
    }

    public override ActionEnum Process() {
        entity.transform.LookAt(entity.Target);
        return ActionEnum.STATUS_COMPLETED;
    }
}
