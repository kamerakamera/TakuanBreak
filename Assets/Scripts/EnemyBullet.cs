using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {
    [SerializeField]
    Rigidbody rb;
    [SerializeField]
    GameObject explodePrefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other) {
        if(other.tag != "Takuan" && other.tag != "Boss") {
            Instantiate(explodePrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        
    }
}
