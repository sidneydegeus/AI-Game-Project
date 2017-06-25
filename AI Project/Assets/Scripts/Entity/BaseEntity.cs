using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEntity : MonoBehaviour {

    // Think and entity related behaviours... defined in concrete entity at Start method!
    internal IThinkBehaviour thinkBehaviour;
    internal Dictionary<BehaviourEnum, IEntityBehaviour> entityBehaviours;
    internal Action Think;
    internal Animator animator;

    // Field of view of the entity
    internal FieldOfView fieldOfView;

    [HideInInspector]
    public bool Selected;

    internal EntityStats Stats;

    [HideInInspector]
    public List<string> ActionDescriptionList;

    internal List<Item> Inventory;

    internal NewHuman LastHitBy;
    protected float removeLastHitBy = 10.0f;

    internal Vector3 TargetPosition;
    internal Transform Target;

    public float MaxAttackCooldown = 1.0f;
    public float MinAttackCooldown = 0.75f;
    internal float CurrentAttackCooldown = 0.0f;

    protected virtual void Start() {
        Think = new Think(this);
        entityBehaviours = new Dictionary<BehaviourEnum, IEntityBehaviour>();
        animator = GetComponent<Animator>();
        fieldOfView = GetComponentInChildren<FieldOfView>();
        Stats = new EntityStats();
        Inventory = new List<Item>();
        ActionDescriptionList = new List<string>();
    }

    protected virtual void Update() {
        Think.Process();
    }

    public abstract void Attack();
}