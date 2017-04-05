using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadHumanBehaviour : IHumanBehaviour {

    public BadHumanBehaviour() {
        WorldManager.BadHumanCount++;
    }

    public Action Attack() {
        // a bad human attacks people for money?
        throw new NotImplementedException();
    }

    public Action Eat() {
        // a bad human goes to grocery store to steal food?
        throw new NotImplementedException();
    }

    public Action Purchase() {
        throw new NotImplementedException();
    }

    public Action Rest() {
        throw new NotImplementedException();
    }
}
