using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadHumanBehaviour : IHumanBehaviour {

    public BadHumanBehaviour() {
        WorldManager.BadHumanCount++;
    }

    public void Attack() {
        // a bad human attacks people for money?
        throw new NotImplementedException();
    }

    public void Eat() {
        // a bad human goes to grocery store to steal food?
        throw new NotImplementedException();
    }

    public void Purchase() {
        throw new NotImplementedException();
    }

    public void Rest() {
        throw new NotImplementedException();
    }
}
