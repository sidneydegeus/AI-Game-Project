using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadHumanBehaviour : IHumanBehaviour {

    public BadHumanBehaviour() {
        WorldManager.BadHumanCount++;
    }

    public void PerformBehaviour() {
        throw new NotImplementedException();
    }
}
