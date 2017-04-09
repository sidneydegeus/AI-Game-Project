using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Human : MovingEntity {

    public Think Think;

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
    public List<string> ActionDescriptionList;

    [HideInInspector]
    public List<Item> Inventory;

    void Start() {
        HumanBehaviour = new GoodHumanBehaviour(this);
        ActionDescriptionList = new List<string>();
        Inventory = new List<Item>();
        Think = new Think(this);
        SetHumanValues();

        StartCoroutine("FindTargetsWithDelay", 2);
        StartCoroutine(Tick());
        Selected = false;
    }

    void Update() {
        Think.Process();
        //ActionDescriptionList.Clear();
        //ActionDescriptionList.Add(think.Description);
        //foreach (string thinkDesc in think.DescriptionList) {
        //    ActionDescriptionList.Add(thinkDesc);
        //}
      //  transform.rotation = Quaternion.LookRotation(currentWaypoint);
        //if (currentWaypoint != Vector3.zero)
           
        // maybe some physics calculation here to reduce health upon hit?
    }

    void OnDestroy() {
        Think.Terminate();
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