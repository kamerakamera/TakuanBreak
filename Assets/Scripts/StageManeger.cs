using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManeger : MonoBehaviour {
    GameObject[] takuan;
    public GameObject takuanPrefab;
    float createNumber, takuanCreateTime = 0;
    bool isTakuanCreate;
    public static int score = 0;
    // Use this for initialization
    void Start () {
        takuan = GameObject.FindGameObjectsWithTag("Takuan");
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
            Instantiate(takuanPrefab,new Vector3(Random.Range(12f,-12f),2f, Random.Range(7f, -7f)),Quaternion.identity);
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
}
