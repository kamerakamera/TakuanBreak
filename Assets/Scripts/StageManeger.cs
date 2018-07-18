using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManeger : MonoBehaviour {
    GameObject[] takuan;
    Vector3 playerPosition;
    public GameObject player,takuanPrefab;
    float createPositonX, createPositionZ;
    float createNumber, takuanCreateTime = 0,prohibitedArea = 1;
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
        if (isTakuanCreate) {
            TakuanCreate();
        }
    }
    public void Search() {
        takuan = GameObject.FindGameObjectsWithTag("Takuan");
        if(takuan.Length <= 5) {
            isTakuanCreate = true;
        }
    }

    void TakuanCreate() {
        takuan = GameObject.FindGameObjectsWithTag("Takuan");
        takuanCreateTime += Time.fixedDeltaTime;

        if (takuanCreateTime >= 0.1) {
            TakuanCreatePosition();
            Debug.Log("X = " + createPositonX + " " + "Y = " + createPositionZ);
            Instantiate(takuanPrefab,new Vector3(createPositonX,2f, createPositionZ),Quaternion.identity);
            takuanCreateTime = 0;
            if(takuan.Length >= 4) {
                isTakuanCreate = false;
                if (score % 2 == 0) {
                    Enemy.hard++;
                }
            }
        }
    }

    public void AddScore() {
        score++;
        //Debug.Log(score);
    }

    public void TakuanCreatePosition() {
        playerPosition = player.transform.position;
        if (12f - Mathf.Abs(playerPosition.x) <= prohibitedArea) {
            if(playerPosition.x <= 0) {
                createPositonX = Random.Range(12f,-12f + prohibitedArea);
            } else if(playerPosition.x >= 0){
                createPositonX = Random.Range(12f - prohibitedArea,-12f);
            }
        }else if(playerPosition.x > prohibitedArea/2) {
            float randX = Random.Range(12f - prohibitedArea,-12f);
            if(playerPosition.x + (prohibitedArea/2) >= randX && playerPosition.x - (prohibitedArea / 2) <= randX) {
                randX += prohibitedArea;
            }
            createPositonX = randX;
        }

        if (12f - Mathf.Abs(playerPosition.z) <= prohibitedArea) {
            if (playerPosition.z <= 0) {
                createPositionZ = Random.Range(7f, -7f + prohibitedArea);
            } else if (playerPosition.z >= 0) {
                createPositionZ = Random.Range(7f - prohibitedArea, -7f);
            }
        } else if (playerPosition.z > prohibitedArea / 2) {
            float randZ = Random.Range(7f - prohibitedArea, -7f);
            if (playerPosition.z + (prohibitedArea / 2) >= randZ && playerPosition.z - (prohibitedArea / 2) <= randZ) {
                randZ += prohibitedArea;
            }
            createPositionZ = randZ;
        }
    }
}
