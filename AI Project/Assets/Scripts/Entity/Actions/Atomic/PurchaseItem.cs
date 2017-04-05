using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

    class PurchaseItem : Action {

        Human human;
    float buyingItem;

        public PurchaseItem(MovingEntity _entity) : base(_entity) {
            Description = "Purchasing item";
            human = (Human)entity;
        buyingItem = 1.00f;
    }

        public override void Activate() {
            Status = ActionEnum.STATUS_ACTIVE;
        }

        public override ActionEnum Process() {
            buyingItem -= 0.02f;
            if (buyingItem <= 0.00f) {
                human.HumanBehaviour.Purchase();
                human.Hunger -= 5;
                Status = ActionEnum.STATUS_COMPLETED;
            }
            return Status;
        }

        public override void Terminate() {
            throw new NotImplementedException();
        }
    }

