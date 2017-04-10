using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Human : MovingEntity {

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
    public Human LastHitBy;
    float removeLastHitBy = 10.0f;


    public GameObject projectilePrefab;

    [HideInInspector]
    public List<string> ActionDescriptionList;

    [HideInInspector]
    public List<Item> Inventory;

    [HideInInspector]
    public Think Think;

    void Start() {
        HumanBehaviour = new GoodHumanBehaviour(this);
        ActionDescriptionList = new List<string>();
        Inventory = new List<Item>();
        Think = new Think(this);
        SetHumanValues();
        Selected = false;
        DisplayFovToggle = true;

        StartCoroutine("FindTargetsWithDelay", 2);
        StartCoroutine(Tick());
    }

    void Update() {
        if (Health <= 0) {
            Destroy(gameObject);
        }
        if (removeLastHitBy <= 0.00f) {
            LastHitBy = null;
            removeLastHitBy = 10.0f;
        }
        if (LastHitBy != null) {
            removeLastHitBy -= Time.deltaTime;
        }
        Think.Process();         
        // maybe some physics calculation here to reduce health upon hit?

        if (Selected && DisplayFovToggle) {
            DrawFieldOfView();
        } else {
            ClearViewMesh();
        }

        if (Selected && DisplayPathfindToggle) {
            DisplayWaypoints();
        }
    }

    void OnDestroy() {
        if (LastHitBy != null) {
            LastHitBy.Money += 500;
        }
        Think.Terminate();
        StopCoroutine(Tick());
        if (humanBehaviour.GetType() == typeof(GoodHumanBehaviour)) {
            WorldManager.GoodHumanCount--;
        }
        if (humanBehaviour.GetType() == typeof(BadHumanBehaviour)) {
            WorldManager.BadHumanCount--;
        }
    }

    public void ShootProjectile(Transform target) {
        GameObject tempProjectile = Instantiate(projectilePrefab) as GameObject;
        tempProjectile.GetComponent<Projectile>().Owner = this;
        Vector3 targetDirection = transform.position;
        targetDirection.y += 0.5f;
        tempProjectile.transform.position = targetDirection + transform.forward;
        tempProjectile.transform.LookAt(target);
        Rigidbody rb = tempProjectile.GetComponent<Rigidbody>();
        rb.velocity = tempProjectile.transform.forward * 10;
    }

    void SetHumanValues() {
        //System.Random r = new System.Random();
        Money = 0;
        Hunger = 0;
        Health = 100;
    }

    IEnumerator Tick() {
        while (true) {
            humanBehaviour.Tick();
            yield return new WaitForSeconds(3.0f);
        }
    }
}