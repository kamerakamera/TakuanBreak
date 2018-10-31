using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBullet : Bullet {
    public GameObject enemyObj;
    //public Vector3 enemyPosition;

	// Use this for initialization
	void Start () {
        //bulletSpeed = bulletSpeed * 0.1f;
        rb = GetComponent<Rigidbody>();
        deleteTime = 30;
        deleteCount = 0;
    }
	
	// Update is called once per frame
	void Update () {
        HomingEnemy();
	}



    protected override void FixedUpdate() {
        //enemyPosition = enemyObj.transform.position;
        //transform.LookAt(enemyPosition);
        //ShotBullet(bulletSpeed);
    }

    public void SetTargetEnemy(GameObject targetObj) {
        enemyObj = targetObj;
    }

    public void HomingEnemy() {
        if(enemyObj == null) {
            Delete();
        } else {
            rb.velocity = (enemyObj.transform.position - transform.position).normalized * bulletSpeed;
        }
    } 

    protected override void OnTriggerEnter(Collider col) {
        if (col.tag == "Takuan") {
            Enemy enemy = col.GetComponent<Enemy>();
            enemy.Damege(5);
            Delete();
        } else if (col.tag == "Boss") {
            Boss boss = col.GetComponent<Boss>();
            boss.Damege(5);
            Delete();
        } else if (col.tag == "Stage") {
            Delete();
        } else if (col.tag == "EnemyBullet") {
            Delete();
        }
    }
}
