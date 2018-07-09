using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour {
    public Image StartImage;
    float waitTime;
    bool wait;
    public Text rotateSensitivity;
    // Use this for initialization
    void Start () {
        wait = true;
        rotateSensitivity.enabled = false;
        rotateSensitivity.text = "感度 " + Player.rotateSensitivityPower;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Q)) {
            //Debug.Log("hoge");
            if (!rotateSensitivity.enabled) {
                rotateSensitivity.enabled = true;
            }
            else if (rotateSensitivity.enabled) {
                rotateSensitivity.enabled = false;
            }
        }
        if(rotateSensitivity.enabled == true) {
            if (Input.GetKeyDown(KeyCode.RightArrow) && Player.rotateSensitivityPower < 10) {
                Player.rotateSensitivityPower++;
                rotateSensitivity.text = "感度 " + Player.rotateSensitivityPower;
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) && Player.rotateSensitivityPower > 1) {
                Player.rotateSensitivityPower--;
                rotateSensitivity.text = "感度 " + Player.rotateSensitivityPower;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space) && !wait) {
            SceneManager.LoadScene("Main");
        }
        WaitTime();
	}

    void WaitTime() {
        waitTime += Time.deltaTime;
        if (waitTime >= 1) {
            wait = false;
        }
    }
}
