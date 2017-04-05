using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WorldManager : MonoBehaviour {

    public GameObject[] spawners = new GameObject[4];
    public Human human;

    public Text HumanCountText;
    public Text GoodHumanCountText;
    public Text BadHumanCountText;
    public Text PoliceHumanCountText;

    public Button AddHumanButton;
    public Button ResetFieldButton;

    //public static int HumanCount = 0;
    public static int GoodHumanCount = 0;
    public static int BadHumanCount = 0;
    public static int PoliceHumanCount = 0;


	// Use this for initialization
	void Start () {
        AddHumanButton.onClick.AddListener(AddHuman);
        ResetFieldButton.onClick.AddListener(ResetField);
        //StartCoroutine(SpawnHuman());
    }

    void Update() {
        HumanCountText.text = (GoodHumanCount + BadHumanCount + PoliceHumanCount).ToString();
        GoodHumanCountText.text = GoodHumanCount.ToString();
        BadHumanCountText.text = BadHumanCount.ToString();
        PoliceHumanCountText.text = PoliceHumanCount.ToString();
    }

    IEnumerator SpawnHuman() {
        //AddHuman();
        //yield return null;
        while (true) {
            AddHuman();
            yield return new WaitForSeconds(5.0f);
        }
    }

    void AddHuman() {
        System.Random r = new System.Random();
        int index = r.Next(0, 4);
        //Human spawnedHuman = Instantiate(human, spawners[index].transform.position, Quaternion.identity) as Human;
        Human spawnedHuman = Instantiate(human, spawners[index].transform.position, Quaternion.identity) as Human;
    }

    void ResetField() {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}