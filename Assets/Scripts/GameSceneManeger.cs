using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneManeger : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void CloseGame() {
        //Application.Quit();
    }
}
