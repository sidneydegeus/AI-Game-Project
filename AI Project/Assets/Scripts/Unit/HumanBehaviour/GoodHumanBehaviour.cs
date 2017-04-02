using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodHumanBehaviour : IHumanBehaviour {

    public GoodHumanBehaviour() {
        WorldManager.GoodHumanCount++;
    }

    public void PerformBehaviour() {
        throw new NotImplementedException();
    }
}
