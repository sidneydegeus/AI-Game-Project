using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewHuman : MovingEntity {

    public GameObject projectilePrefab;
    public double MoneyPerHealth = 5;

    protected override void Start () {
        base.Start();
        WorldManager.HumanCount++;
        thinkBehaviour = new HumanThinkBehaviour(this);
        entityBehaviours[BehaviourEnum.WANDER_BEHAVIOUR] = new WanderBehaviour(this);
        entityBehaviours[BehaviourEnum.FOLLOW_BEHAVIOUR] = new FollowpathBehaviour(this);
        entityBehaviours[BehaviourEnum.ATTACK_BEHAVIOUR] = new AttackBehaviour(this);
        StartCoroutine(Tick());
    }

    // Update is called once per frame
    protected override void Update () {
        base.Update();

        if (Stats.Health <= 0) {
            Destroy(gameObject);
        }
        if (removeLastHitBy <= 0.00f) {
            LastHitBy = null;
            removeLastHitBy = 10.0f;
        }
        if (LastHitBy != null) {
            removeLastHitBy -= Time.deltaTime;
        }

        if (Selected && DisplayPathfindToggle) {
            DisplayWaypoints();
        }
    }

    void OnDestroy() {
        if (LastHitBy != null) {
            LastHitBy.Stats.Money += 500;
        }
        Think.Terminate();
        StopCoroutine(Tick());
        WorldManager.HumanCount--;
    }

    public override void Attack() {
        if (Target != null) {
            ShootProjectile(Target);
        }
    }

    void ShootProjectile(Transform target) {
        GameObject tempProjectile = Instantiate(projectilePrefab) as GameObject;
        tempProjectile.GetComponent<Projectile>().Owner = this;
        Vector3 targetDirection = transform.position;
        targetDirection.y += 0.5f;
        tempProjectile.transform.position = targetDirection + transform.forward;
        tempProjectile.transform.LookAt(target);
        Rigidbody rb = tempProjectile.GetComponent<Rigidbody>();
        rb.velocity = tempProjectile.transform.forward * 10;
    }

    IEnumerator Tick() {
        while (true) {
            HumanStatsTick();
            yield return new WaitForSeconds(3.0f);
        }
    }

    void HumanStatsTick() {
        Stats.Money += 2;
        Stats.Hunger += 1;
        if (Stats.Hunger >= 100) {
            Stats.Health -= 1;
        }
        else if (Stats.Hunger <= 50) {
            Stats.Health += 1;
            if (Stats.Health > 100) {
                Stats.Health = 100;
            }
        }
    }
}
