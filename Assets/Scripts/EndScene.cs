using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndScene : MonoBehaviour {
    StageManeger stageManeger;
    public Text scoreText;
    float waitTime;
    bool wait;
	// Use this for initialization
	void Start () {
        scoreText.text = "Score " + StageManeger.score;
        wait = true;
	}
	
	// Update is called once per frame
	void Update () {
        WaitTime();
        if(!wait && Input.GetKeyDown(KeyCode.Space)) {
            SceneManager.LoadScene("Start");
        }
	}

    void WaitTime() {
        waitTime += Time.deltaTime;
        if(waitTime >= 3) {
            wait = false;
        }
    }
}
