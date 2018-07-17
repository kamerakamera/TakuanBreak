using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBullet : Bullet {
    public GameObject p;
    public Vector3 enemyPosition;

	// Use this for initialization
	void Start () {
        p = GameObject.Find("Player");
        bulletSpeed = bulletSpeed * 0.8f;
        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        enemyPosition = p.transform.position;
        transform.LookAt(enemyPosition);
        //rb.velocity = (enemyPosition - transform.position).normalized * bulletSpeed;
	}

    /*void FixedUpdate() {
        
    }*/
}
