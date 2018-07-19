using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeadParticle : MonoBehaviour {
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
}
