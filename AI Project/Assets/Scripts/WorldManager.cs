using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour {

    public GameObject spawner;
    public Human human;

    public static int HumanCount = 0;
	// Use this for initialization
	void Start () {
        StartCoroutine(SpawnHuman());
    }

    IEnumerator SpawnHuman() {
        while (true) {
            Human spawnedHuman = Instantiate(human, spawner.transform.position, Quaternion.identity) as Human;
            Debug.Log("human count = " + HumanCount);
            yield return new WaitForSeconds(5.0f);
        }
    }
}
