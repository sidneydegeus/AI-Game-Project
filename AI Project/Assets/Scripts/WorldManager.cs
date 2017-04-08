using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WorldManager : MonoBehaviour {

    public GameObject[] spawners = new GameObject[4];
    public Human prefabHuman;

    public Text HumanCountText;
    public Text GoodHumanCountText;
    public Text BadHumanCountText;

    public Text HumanSelectedText;
    public Text HumanBehaviourText;
    public Text HumanHealthText;
    public Text HumanHungerText;
    public Text HumanMoneyText;

    public Button AddHumanButton;
    public Button ResetFieldButton;

    //public static int HumanCount = 0;
    public static int GoodHumanCount = 0;
    public static int BadHumanCount = 0;

    Human selectedHuman;
    public List<Text> actionTextList;
    GameObject thinkingTextHolder;

	// Use this for initialization
	void Start () {
        Application.runInBackground = true;
        AddHumanButton.onClick.AddListener(AddHuman);
        ResetFieldButton.onClick.AddListener(ResetField);
        thinkingTextHolder = GameObject.Find("Thinking");
        //StartCoroutine(SpawnHuman());
    }

    void Update() {
        UpdateGUI();

        if (Input.GetMouseButtonDown(0)) {

            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
            if (hit) {
                //Debug.Log("Hit " + hitInfo.transform.gameObject.name);
                if (hitInfo.transform.gameObject.tag == "Human") {
                    selectedHuman = hitInfo.transform.gameObject.GetComponent<Human>();
                    selectedHuman.Selected = true;
                } else {
                    selectedHuman = null;
                    HumanSelectedText.text = "No";
                    HumanBehaviourText.text = "";
                    HumanHealthText.text = "0";
                    HumanHungerText.text = "0";
                    HumanMoneyText.text = "0";
                }
            } else {
                Debug.Log("No hit");
            }
        }
    }

    IEnumerator SpawnHuman() {
        while (true) {
            if (GoodHumanCount + BadHumanCount < 10) { 
                AddHuman();
            }
            yield return new WaitForSeconds(5.0f);
        }
    }

    void AddHuman() {
        System.Random r = new System.Random();
        int index = r.Next(0, 4);
        //Human spawnedHuman = Instantiate(human, spawners[index].transform.position, Quaternion.identity) as Human;
        Human spawnedHuman = Instantiate(prefabHuman, spawners[index].transform.position, Quaternion.identity) as Human;
    }

    void ResetField() {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    void UpdateGUI() {
        HumanCountText.text = (GoodHumanCount + BadHumanCount).ToString();
        GoodHumanCountText.text = GoodHumanCount.ToString();
        BadHumanCountText.text = BadHumanCount.ToString();

        if (selectedHuman != null) {
            HumanSelectedText.text = "Yes";
            HumanBehaviourText.text = selectedHuman.HumanBehaviour.Description;
            HumanHealthText.text = selectedHuman.Health.ToString();
            HumanHungerText.text = selectedHuman.Hunger.ToString();
            HumanMoneyText.text = selectedHuman.Money.ToString();

            foreach (Text text in actionTextList) {
                text.text = "";
            }

            int i = 0;
            foreach (Action action in selectedHuman.ActionList) {
                if (i < actionTextList.Count) {
                    actionTextList[i].text = action.Description;
                    i++;
                } else {
                    break;
                }
            }
        } 
    }
}