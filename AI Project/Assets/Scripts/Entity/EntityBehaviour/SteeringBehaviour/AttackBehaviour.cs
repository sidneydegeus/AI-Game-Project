using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class AttackBehaviour : IEntityBehaviour {

    protected BaseEntity entity;

    public AttackBehaviour(BaseEntity _entity) {
        entity = _entity;
    }

    public void Init() {

    }

    public ActionEnum Process() {
        entity.Attack();
        return ActionEnum.STATUS_COMPLETED;
    }
}
