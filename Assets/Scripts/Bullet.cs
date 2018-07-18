using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    protected Rigidbody rb;
    protected float deleteTime = 30f,deleteCount;
    protected float bulletSpeed = 10;
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        deleteCount = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    protected virtual void FixedUpdate() {
        ShotBullet(bulletSpeed);
        deleteCount += Time.fixedDeltaTime;

        if (deleteCount >= deleteTime) {
            deleteCount = 0;
            Debug.Log(deleteCount);
            Delete();
        }
    }

    protected void ShotBullet(float speed) {
        rb.velocity = transform.forward.normalized * speed;
    }

    protected void Delete() {
        //Debug.Log(this.gameObject);
        GameObject.Destroy(this.gameObject);
    }

    protected virtual void OnTriggerEnter(Collider col) {
        if(col.tag == "Takuan") {
            Enemy enemy = col.GetComponent<Enemy>();
            enemy.Damege();
            Delete();
        }
        else if (col.tag == "Stage") {
            Delete();
        }
    }

}
