using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadHumanBehaviour : IHumanBehaviour {

    private string description;
    Human human;

    public BadHumanBehaviour() {
        WorldManager.BadHumanCount++;
        Description = "Bad";
    }

    public string Description {
        get {
            return description;
        }

        set {
            description = value;
        }
    }

    public void Attack() {
        // a bad human attacks people for money?
        throw new NotImplementedException();
    }

    public void Eat() {
        // a bad human goes to grocery store to steal food?
        throw new NotImplementedException();
    }

    public void Purchase(Item item) {
        throw new NotImplementedException();
    }

    public void Rest() {
        throw new NotImplementedException();
    }
}
