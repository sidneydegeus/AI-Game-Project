using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodHumanBehaviour : IHumanBehaviour {

    public GoodHumanBehaviour() {
        WorldManager.GoodHumanCount++;
    }

    public void Attack() {
        // a good human doesn't attack, but defends instead
        throw new NotImplementedException();
    }

    public void Eat() {
        // a good human goes to the grocery store to buy food and then eat it
        throw new NotImplementedException();
    }

    public void Purchase() {
        Debug.Log("i just bought food");
    }

    public void Rest() {
        // idk just standard rest?
        throw new NotImplementedException();
    }
}
