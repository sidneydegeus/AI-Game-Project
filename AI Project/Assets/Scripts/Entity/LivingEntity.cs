using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour {

    public float viewRadius;

    public LayerMask targetMask;

    [HideInInspector]
    public List<Transform> visibleTargets = new List<Transform>();

    protected IEnumerator ScanRadius() {
        while (true) {
            yield return new WaitForSeconds(.2f);
            FindVisibleTargets();
        }
    }

    void FindVisibleTargets() {
        visibleTargets.Clear();
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);
        // we start from 1, because physics.overlapsphere detects this gameobject as well... so by starting from 1
        // we ignore adding itself to an array of humans in a radius
        for (int i = 1; i < targetsInViewRadius.Length; i++) {
            Transform target = targetsInViewRadius[i].transform;
            visibleTargets.Add(target);
        }
    }
}
