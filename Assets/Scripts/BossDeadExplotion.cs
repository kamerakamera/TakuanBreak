﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeadExplotion : MonoBehaviour {
    float deleteCount, deleteTime = 10;
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        deleteCount += Time.deltaTime;
        if (deleteCount >= deleteTime) {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider col) {
        if(col.tag == "Player") {
            Player p = col.gameObject.GetComponent<Player>();
            p.Damege(100);
        }
    }
}
