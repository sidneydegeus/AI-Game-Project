using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

    class ExitStore : Action {

        Renderer render;
        Human human;
    float fadeOut;

        public ExitStore(MovingEntity _entity) : base(_entity) {
            Description = "Entering store";
            human = (Human)entity;
            render = human.transform.GetChild(0).GetComponent<Renderer>();
        fadeOut = 0.00f;
    }

        public override void Activate() {
            Status = ActionEnum.STATUS_ACTIVE;
        }

        public override ActionEnum Process() {
            fadeOut += 0.02f;
            if (fadeOut >= 1.00f) {
                render.enabled = true;
                Status = ActionEnum.STATUS_COMPLETED;
            Debug.Log("exiting store");
            }
            return Status;
        }

        public override void Terminate() {
            throw new NotImplementedException();
        }
    }

