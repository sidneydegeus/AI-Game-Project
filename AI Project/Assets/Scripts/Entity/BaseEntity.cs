using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEntity : MonoBehaviour {

    // Think and entity related behaviours... defined in concrete entity at Start method!
    internal IThinkBehaviour thinkBehaviour;
    internal Dictionary<BehaviourEnum, IEntityBehaviour> entityBehaviours;
    internal Action Think;
    internal Animator animator;

    // Field of view of the entity
    internal FieldOfView fieldOfView;

    //public float viewRadius;
    //[Range(0, 360)]
    //public float viewAngle;

    //public LayerMask targetMask;
    //public LayerMask obstacleMask;

    //[HideInInspector]
    //public Transform LockedTarget;

    //public float meshResolution;
    //public int edgeResolveIterations;
    //public float edgeDstThreshold;

    //public MeshFilter viewMeshFilter;
    //Mesh viewMesh;

    [HideInInspector]
    public bool Selected;

    ////[HideInInspector]
    //public List<Transform> visibleTargets = new List<Transform>();

    //[HideInInspector]
    //public bool DisplayFovToggle;

    internal EntityStats Stats;

    [HideInInspector]
    public List<string> ActionDescriptionList;

    internal NewHuman LastHitBy;
    float removeLastHitBy = 10.0f;

    protected virtual void Start() {
        Think = new Think(this);
        entityBehaviours = new Dictionary<BehaviourEnum, IEntityBehaviour>();
        animator = GetComponent<Animator>();
        fieldOfView = GetComponentInChildren<FieldOfView>();
        Stats = new EntityStats();
        ActionDescriptionList = new List<string>();
    }

    protected virtual void Update() {
        Think.Process();
    }
}