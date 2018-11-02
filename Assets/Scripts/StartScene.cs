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
            if (Input.GetKeyDown(KeyCode.RightArrow) && Player.rotateSensitivityPower < 6) {
                Player.rotateSensitivityPower += 0.1f;
                rotateSensitivity.text = "感度 " + Player.rotateSensitivityPower.ToString("F1");
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) && Player.rotateSensitivityPower > 2) {
                Player.rotateSensitivityPower -= 0.1f;
                rotateSensitivity.text = "感度 " + Player.rotateSensitivityPower.ToString("F1");
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
