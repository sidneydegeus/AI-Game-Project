using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

    class EnterStore : Action {

        Renderer render;
        Human human;
    float fadeOut;

        public EnterStore(MovingEntity _entity) : base(_entity) {
            Description = "Entering store";
            human = (Human)entity;
            render = human.GetComponent<Renderer>();
         fadeOut = 1.00f;
    }

        public override void Activate() {
            Status = ActionEnum.STATUS_ACTIVE;
        }

        public override ActionEnum Process() {
            fadeOut -= 0.02f;
            if (fadeOut <= 0) {
                render.enabled = false;
                Status = ActionEnum.STATUS_COMPLETED;
            Debug.Log("entering store");
            }
            return Status;
        }

        public override void Terminate() {
            throw new NotImplementedException();
        }
    }
