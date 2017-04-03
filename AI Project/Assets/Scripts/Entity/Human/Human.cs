using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Human : MovingEntity {

    Think think;
    private IHumanBehaviour humanBehaviour;
    public IHumanBehaviour HumanBehaviour {
        get {
            return humanBehaviour;
        }
        set {
            if (humanBehaviour != value && humanBehaviour != null) {
                if (humanBehaviour.GetType() == typeof(GoodHumanBehaviour)) {
                    WorldManager.GoodHumanCount--;
                }
                if (humanBehaviour.GetType() == typeof(BadHumanBehaviour)) {
                    WorldManager.BadHumanCount--;
                }
            }
            humanBehaviour = value;
        }
   }

    int hunger;
    int money;
    int health;
    // some specific entity values?
    
    // behaviour variable to make use of a strategy pattern

    void Start() {
        HumanBehaviour = new GoodHumanBehaviour();
        think = new Think(this);
        SetHumanValues();
        StartCoroutine(ScanRadius());
        //StartCoroutine(Tick());
    }

    void Update() {
        think.Process();
    }

    void OnDestroy() {
        think.Terminate();
        StopCoroutine(Tick());
        if (humanBehaviour.GetType() == typeof(GoodHumanBehaviour)) {
            WorldManager.GoodHumanCount--;
        }
        if (humanBehaviour.GetType() == typeof(BadHumanBehaviour)) {
            WorldManager.BadHumanCount--;
        }
        if (humanBehaviour.GetType() == typeof(PoliceHumanBehaviour)) {
            WorldManager.PoliceHumanCount--;
        }
        //WorldManager.HumanCount--; 
    }

    void SetHumanValues() {
        System.Random r = new System.Random();
        money = r.Next(500, 6000);
        hunger = 0;
        health = 100;
    }

    IEnumerator Tick() {
        while (true) {
            money += 5;
            hunger += 1;
            if (hunger >= 100) {
                health -= 1;
            }
            else if (hunger <= 50) {
                health += 1;
            }

            yield return new WaitForSeconds(3.0f);
        }

    }
}