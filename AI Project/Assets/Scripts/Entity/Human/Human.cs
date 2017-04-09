﻿using UnityEngine;
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

    GameObject projectilePrefab;

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
        projectilePrefab = Resources.Load("projectile") as GameObject;

        StartCoroutine("FindTargetsWithDelay", 2);
        StartCoroutine(Tick());
    }

    void Update() {
        if (Health <= 0) {
            Destroy(gameObject);
        }
        Think.Process();         
        // maybe some physics calculation here to reduce health upon hit?
        if (Input.GetKeyDown("p")) {
            GameObject tempProjectile = Instantiate(projectilePrefab) as GameObject;
            tempProjectile.transform.position = transform.position + transform.forward * 2;
            Rigidbody rb = tempProjectile.GetComponent<Rigidbody>();
            rb.velocity = transform.forward * 10;
        }

        if (Selected && DisplayFovToggle) {
            DrawFieldOfView();
        } else {
            ClearViewMesh();
        }
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
        Money = r.Next(125, 250);
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