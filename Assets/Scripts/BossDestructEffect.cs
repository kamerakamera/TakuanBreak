using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDestructEffect : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Growing();
        ReduceColor();
        DeleteEffect();
    }

    void Growing() {
        this.transform.localScale -= new Vector3(0.9f, 0.9f, 0.9f);
    }

    void ReduceColor() {
        GetComponent<Renderer>().material.color -= new Color(0, 0, 0, 0.01f);
    }

    void DeleteEffect() {
        if(this.transform.localScale.x <= 0.001f) {
            Destroy(this.gameObject);
        }
    }
}
