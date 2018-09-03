using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManeger : MonoBehaviour {
    GameObject[] takuan;
    public GameObject bossTakuan;
    Vector3 playerPosition;
    public GameObject player,takuanPrefab;
    float createPositionX, createPositionZ;
    float createNumber,prohibitedArea = 1;
    bool isTakuanCreate;
    public static int score = 0;
    // Use this for initialization
    void Start () {
        takuan = GameObject.FindGameObjectsWithTag("Takuan");
        player = GameObject.Find("Player");
        score = 0;
        Enemy.hard = 0;
	}
	
	// Update is called once per frame
	void Update () {

    }

    private void FixedUpdate() {
        /*if (isTakuanCreate) {
            TakuanCreate();
        }*/
    }
    public void Search() {
        takuan = GameObject.FindGameObjectsWithTag("Takuan");
        if(takuan.Length < 5) {
            //isTakuanCreate = true;
            TakuanCreate();
        } 
    }

    void TakuanCreate() {
        TakuanCreatePosition();
        Debug.Log("X = " + createPositionX + " " + "Y = " + createPositionZ);
        /*if (takuan.Length >= 5) {
            isTakuanCreate = false;
        }*/
        Instantiate(takuanPrefab,new Vector3(createPositionX,2f, createPositionZ),Quaternion.identity);
        takuan = GameObject.FindGameObjectsWithTag("Takuan");
        if (takuan.Length > 5) {
            //isTakuanCreate = false;
            if (score % 6 == 0) {
                Enemy.hard++;
            }
        }
        
    }

    public void AddScore() {
        score++;
        if(score % 40 == 0) {
            BossStage();
        }
        //Debug.Log(score);
    }

    public void TakuanCreatePosition() {
        do {
            createPositionX = Random.Range(12f, -12f);
        }
        while (createPositionX >= playerPosition.x - prohibitedArea && createPositionX <= playerPosition.x + prohibitedArea);
        do {
            createPositionZ = Random.Range(7f, -7f);
        }
        while (createPositionZ >= playerPosition.z - prohibitedArea && createPositionZ <= playerPosition.z + prohibitedArea);
    }

    public void BossStage() {
        Instantiate(bossTakuan, new Vector3(0, 5.28f, 0), Quaternion.identity);
        
    }
}
