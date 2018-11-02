using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateFieldBullet : MonoBehaviour {
    GameObject fieldPrefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetFieldPrefab(GameObject setField) {
        fieldPrefab = setField;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Stage") {
            Instantiate(fieldPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
