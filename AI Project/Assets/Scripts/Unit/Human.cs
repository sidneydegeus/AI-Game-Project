using UnityEngine;
using System.Collections;
using Assets.Scripts.Unit;
using System.Collections.Generic;
using System;

public class Human : MovingEntity {

    Think think;

    int hunger;
    int money;
    int health;
    // some specific unit values?
    
    // behaviour variable to make use of a strategy pattern

    void Start() {
        // upon creation, humancount += 1 (maybe put this in awake()?)
        WorldManager.HumanCount++;
        think = new Think(this);
        SetHumanValues();
        //StartCoroutine(Tick());
    }

    void Update() {
        think.Process();
    }

    void OnDestroy() {
        think.Terminate();
        StopCoroutine(Tick());
        WorldManager.HumanCount--; 
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