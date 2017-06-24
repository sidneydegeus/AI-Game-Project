using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public NewHuman Owner;

    void Awake() {
        Destroy(gameObject, 3.0f);
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Human") {
            NewHuman human = collision.gameObject.GetComponentInParent<NewHuman>();
            human.Stats.Health -= 10;
            human.LastHitBy = Owner;
            Destroy(gameObject);
            Debug.Log("hitting a human");
        }
        else {
            Destroy(gameObject);
        }
    }
}
