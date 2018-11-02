using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManeger : MonoBehaviour {
    GameObject[] takuan;
    public GameObject bossTakuan,nowBossTakuan;
    Vector3 playerPosition;
    public GameObject player,takuanPrefab;
    float createPositionX, createPositionZ;
    float createNumber,prohibitedArea = 1;
    public bool isBoss,isDestroy;
    public static float score = 0;
    [SerializeField]
    GameSceneManeger gameSceneManeger;
    // Use this for initialization
    void Start () {
        takuan = GameObject.FindGameObjectsWithTag("Takuan");
        player = GameObject.Find("Player");
        score = 0;
        Enemy.hard = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if(!isDestroy && isBoss) {
            DestroyAllTakuan();
        }
    }

    private void FixedUpdate() {
        
    }
    public void Search() {
        takuan = GameObject.FindGameObjectsWithTag("Takuan");
        if(takuan.Length <= 5) {
            TakuanCreate();
        } 
    }

    void TakuanCreate() {
        TakuanCreatePosition();
        Instantiate(takuanPrefab,new Vector3(createPositionX,2f, createPositionZ),Quaternion.identity);
        takuan = GameObject.FindGameObjectsWithTag("Takuan");
        if(takuan.Length <= 5) {
            TakuanCreate();
        }

        if (score % 5 == 0) {
            Enemy.hard++;
            Debug.Log("speedUp");
        }

    }

    public void AddScore() {
        score++;
    }

    public float GetScore() {
        return score;
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

    public void StartBossStage() {
        nowBossTakuan = Instantiate(bossTakuan, new Vector3(0, 5.28f, 0), Quaternion.identity);
        isBoss = true;
    }

    void DestroyAllTakuan() {
        foreach (GameObject gameobj in takuan) {
            if (gameobj == null) continue;
            gameobj.gameObject.GetComponent<Enemy>().DestroyObject();
        }
        isDestroy = true;
    }

    public void EndBossStage() {
        //Search();
        //isBoss = false;
        gameSceneManeger.ClearGame();
    }
}
