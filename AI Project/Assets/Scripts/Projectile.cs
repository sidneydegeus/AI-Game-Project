using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    void Awake() {
        Destroy(gameObject, 3.0f);
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Human") {
            Human human = collision.gameObject.GetComponentInParent<Human>();
            human.Health -= 10;
            Destroy(gameObject);
            Debug.Log("hitting a human");
        }
        else {
            Destroy(gameObject);
        }
    }
}
