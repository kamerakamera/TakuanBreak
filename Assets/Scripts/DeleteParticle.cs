using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteParticle : MonoBehaviour {
    float deleteCount,deleteTime = 2;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        deleteCount += Time.deltaTime;
        if(deleteCount >= deleteTime) {
            Destroy(this.gameObject);
        }
	}
}
