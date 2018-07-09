using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteParticle : MonoBehaviour {
    float deleteTime;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        deleteTime += Time.deltaTime;
        if(deleteTime >= 2) {
            Destroy(this.gameObject);
        }
	}
}
