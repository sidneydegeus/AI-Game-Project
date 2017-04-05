using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Entity.Actions.Atomic {
    class PurchaseItem : Action {

        public PurchaseItem(MovingEntity _entity) : base(_entity) {
            Description = "Purchasing item";
        }

        public override void Activate() {
            throw new NotImplementedException();
        }

        public override ActionEnum Process() {
            throw new NotImplementedException();
        }

        public override void Terminate() {
            throw new NotImplementedException();
        }
    }
}
