using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WorldManager : MonoBehaviour {

    public GameObject[] spawners = new GameObject[4];
    public Human prefabWoman;
    public Human prefabMan;

    public Text HumanCountText;
    //public Text GoodHumanCountText;
    //public Text BadHumanCountText;

    public Text HumanSelectedText;
    public Text HumanBehaviourText;
    public Text HumanHealthText;
    public Text HumanHungerText;
    public Text HumanMoneyText;

    public Toggle MusicToggle;
    public Toggle DisplayHumanFovToggle;
    public Toggle DisplayHumanPathfindToggle; 

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
        MusicToggle.onValueChanged.AddListener(ToggleMusic);
        DisplayHumanFovToggle.onValueChanged.AddListener(ToggleFov);
        thinkingTextHolder = GameObject.Find("Thinking");
        StartCoroutine(SpawnHuman());
    }

    void Update() {
        UpdateGUI();

        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) {
            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);

            if (Input.GetMouseButtonDown(0)) {
                if (hit) {
                    //Debug.Log("Hit " + hitInfo.transform.gameObject.name);
                    if (hitInfo.transform.gameObject.tag == "Human") {
                        if (selectedHuman != null) {
                            selectedHuman.Selected = false;
                        }
                        selectedHuman = hitInfo.transform.gameObject.GetComponent<Human>();
                        selectedHuman.Selected = true;
                    }
                    else {
                        if (selectedHuman != null) {
                            selectedHuman.Selected = false;
                        }
                        selectedHuman = null;
                        HumanSelectedText.text = "No";
                        HumanBehaviourText.text = "";
                        HumanHealthText.text = "0";
                        HumanHungerText.text = "0";
                        HumanMoneyText.text = "0";
                    }
                }
                else {
                    Debug.Log("No hit");
                }
            }

            if (Input.GetMouseButtonDown(1)) {
                if (hit && selectedHuman != null) {
                    selectedHuman.Think.AddAction(new FollowPath(selectedHuman, hitInfo.transform.position));
                }
            }
        }
    }

    IEnumerator SpawnHuman() {
        while (true) {
            if (GoodHumanCount + BadHumanCount < 15) { 
                AddHuman();
            }
            yield return new WaitForSeconds(5.0f);
        }
    }

    void AddHuman() {
        System.Random r = new System.Random();
        int index = r.Next(0, 4);

        //float x = r.Next(1, 3);
        //if (x == 1)
        //{
        //    Human spawnedHuman = Instantiate(prefabMan, spawners[index].transform.position, Quaternion.identity) as Human;
        //}
        //else
        //{
        //    Human spawnedHuman = Instantiate(prefabWoman, spawners[index].transform.position, Quaternion.identity) as Human;
        //}

        //Human spawnedHuman = Instantiate(human, spawners[index].transform.position, Quaternion.identity) as Human;
        Human spawnedHuman = Instantiate(prefabMan, spawners[index].transform.position, Quaternion.identity) as Human;
    }

    void ResetField() {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    void ToggleMusic(bool value) {
        if (value) {
            gameObject.GetComponent<AudioSource>().mute = false;
        } else {
            gameObject.GetComponent<AudioSource>().mute = true;
        }
    }

    void ToggleFov(bool value) {
        if (value) {
            selectedHuman.DisplayFovToggle = true;
        }
        else {
            selectedHuman.DisplayFovToggle = false;
        }
    }

    void UpdateGUI() {
        HumanCountText.text = (GoodHumanCount + BadHumanCount).ToString();
        //GoodHumanCountText.text = GoodHumanCount.ToString();
        //BadHumanCountText.text = BadHumanCount.ToString();

        if (selectedHuman != null) {
            HumanSelectedText.text = "Yes";
            HumanBehaviourText.text = selectedHuman.HumanBehaviour.Description;
            HumanHealthText.text = selectedHuman.Health.ToString();
            HumanHungerText.text = selectedHuman.Hunger.ToString();
            HumanMoneyText.text = selectedHuman.Money.ToString();

            foreach (Text text in actionTextList) {
                text.text = "";
            }
            selectedHuman.ActionDescriptionList.Clear();
            selectedHuman.Think.GetDescription();
            int i = 0;
            foreach (string description in selectedHuman.ActionDescriptionList) {
                if (i < actionTextList.Count) {
                    actionTextList[i].text = description;
                    i++;
                } else {
                    break;
                }
            }
        } 
    }
}