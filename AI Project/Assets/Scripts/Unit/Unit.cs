using UnityEngine;
using System.Collections;
using Assets.Scripts.Unit;
using System.Collections.Generic;

public class Unit : MovingEntity {

    Think think;

    // some specific unit values?
    
    // behaviour variable to make use of a strategy pattern

    void Start() {
        think = new Think(this);
    }

    void Update() {
        think.Process();
    }
}