using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


class SeekTarget : Action {

    MovingEntity.SeekBehaviour seekBehaviour;

    public SeekTarget(Human _entity) : base(_entity) {
        Description = "Seek target (A)";
    }

    public override void Activate() {
        Status = ActionEnum.STATUS_ACTIVE;
        seekBehaviour = new MovingEntity.SeekBehaviour(entity);
    }

    public override ActionEnum Process() {
        Status = seekBehaviour.Execute();
        return Status;
    }

    public override void Terminate() {
        throw new NotImplementedException();
    }
}

