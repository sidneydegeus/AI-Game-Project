using UnityEngine;
using System.Collections;
using Assets.Scripts.Unit;
using System.Collections.Generic;

public class Unit : MonoBehaviour {


    public Transform target;
    public float Speed { get { return 1; } }
    Vector3[] path;
    int targetIndex;

    Think think;


    void Start() {
        think = new Think(this);//gameObject.AddComponent<Think>();
        //StartCoroutine(Thinking());
        //think = new Think(this);

        //PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
    }

    IEnumerator Thinking() {
        // You will never stop thinking, that's why while true
        while (true) {
            think.Process();
            //yield return new WaitForSeconds(0.1f);
            yield return new WaitForEndOfFrame();
        }
    }

    void Update() {
        think.Process();
    }

    public void OnPathFound(Vector3[] newPath, bool pathSuccessful) {
        if (pathSuccessful) {
            path = newPath;
            targetIndex = 0;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    IEnumerator FollowPath() {
        Vector3 currentWaypoint = path[0];
        while (true) {
            if (transform.position == currentWaypoint) {
                targetIndex++;
                if (targetIndex >= path.Length) {
                    yield break;
                }
                currentWaypoint = path[targetIndex];
            }

            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, Speed * Time.deltaTime);
            yield return null;

        }
    }

    public void OnDrawGizmos() {
        if (path != null) {
            for (int i = targetIndex; i < path.Length; i++) {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(path[i], Vector3.one);

                if (i == targetIndex) {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }
}