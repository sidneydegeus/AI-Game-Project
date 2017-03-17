using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Unit {
    class Think : ActionGroup {

        public Think() {
            Activate();
            Description = "Thinking";
        }

        override
        public int Process() {
            throw new NotImplementedException();
        }
    }
}
