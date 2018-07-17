using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    protected Rigidbody rb;
    protected float deleteTime = 5f,deleteCount;
    protected float bulletSpeed = 10;
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        deleteCount = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void FixedUpdate() {
        Straight();
        deleteCount += Time.fixedDeltaTime;

        if (deleteCount >= deleteTime) {
            Delete();
        }
    }

    void Straight() {
        rb.velocity = transform.forward.normalized * bulletSpeed;
    }

    void Delete() {
        Debug.Log(this.gameObject);
        GameObject.Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider col) {
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
