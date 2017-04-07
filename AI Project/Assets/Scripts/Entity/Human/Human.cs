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

    public int Hunger;
    public int Money;
    public int Health;

    [HideInInspector]
    public bool Selected;

    [HideInInspector]
    public List<string> ActionDescriptionList;
    //public List<Inventory> inventory;

    void Start() {
        HumanBehaviour = new GoodHumanBehaviour(this);
        think = new Think(this);
        SetHumanValues();

        StartCoroutine("FindTargetsWithDelay", 2);
        StartCoroutine(Tick());
        Selected = false;
        ActionDescriptionList = new List<string>();
    }

    void Update() {
        think.Process();
        //ActionDescriptionList.Clear();
        //ActionDescriptionList.Add(think.Description);
        //foreach (string thinkDesc in think.DescriptionList) {
        //    ActionDescriptionList.Add(thinkDesc);
        //}
        transform.rotation = Quaternion.LookRotation(currentWaypoint);
        //if (currentWaypoint != Vector3.zero)
           
        // maybe some physics calculation here to reduce health upon hit?
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
    }

    void SetHumanValues() {
        System.Random r = new System.Random();
        Money = r.Next(500, 6000);
        Hunger = 0;
        Health = 100;
    }

    IEnumerator Tick() {
        while (true) {
            Money += 5;
            Hunger += 1;
            if (Hunger >= 100) {
                Health -= 1;
            }
            else if (Hunger <= 50) {
                Health += 1;
            }

            yield return new WaitForSeconds(3.0f);
        }

    }
}