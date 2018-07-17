using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBullet : Bullet {
    public GameObject enemy;
    public Vector3 enemyPosition;

	// Use this for initialization
	void Start () {
        enemy = GameObject.Find("Player");
        bulletSpeed = bulletSpeed * 0.1f;
        rb = GetComponent<Rigidbody>();
        deleteTime = 30;
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    override protected void FixedUpdate() {
        enemyPosition = enemy.transform.position;
        transform.LookAt(enemyPosition);
        ShotBullet(bulletSpeed);
    }

    protected override void OnTriggerEnter(Collider col) {
        if (col.tag == "Takuan") {
            Enemy enemy = col.GetComponent<Enemy>();
            enemy.Damege();
            base.Delete();
        }
    }
}
