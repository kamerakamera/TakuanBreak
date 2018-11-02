using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDestructEffect : MonoBehaviour {
    [SerializeField]
    StageManeger stageManeger;
    float deleteCount, deleteTime = 5;

	// Use this for initialization
	void Start () {
        stageManeger = GameObject.Find("StageManeger").GetComponent<StageManeger>();
	}
	
	// Update is called once per frame
	void Update () {
        DeleteCount();
    }

    void DeleteCount() {
        deleteCount += Time.deltaTime;
        if(deleteCount >= deleteTime) {
            DeleteEffect();
        }
    }

    void DeleteEffect() {
        stageManeger.EndBossStage();
    }
}
