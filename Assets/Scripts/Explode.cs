using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour {
    [SerializeField]
    Collider explodeCollider;
    [SerializeField]
    float surviveTime;
    float surviveCount;
    // Use this for initialization
    void Start () {
        surviveCount = 0;
	}
	
	// Update is called once per frame
	void Update () {
        DeleteCount();
	}

    void DeleteCount() {
        surviveCount += Time.deltaTime;
        if(surviveCount >= surviveTime) {
            surviveCount = 0;
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider col) {
        if(col.tag == "Player") {
            GameObject playerObj = col.gameObject;
            Player player = playerObj.GetComponent<Player>();
            Rigidbody playerRb = player.GetComponent<Rigidbody>();
            player.Damege(3);
            playerRb.velocity = new Vector3(playerObj.transform.position.x - transform.position.x, playerObj.transform.position.y - transform.position.y, playerObj.transform.position.z - transform.position.z).normalized * 10f;
            explodeCollider.enabled = false;
        }
    }
}
