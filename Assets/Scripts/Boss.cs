using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy {
    public GameObject detonator;
    private float selfDestructTime = 10,selfDestructCount;
    private bool startSelfDestruct,isSelfDestruct,endExplotion;
    Material takuanMaterial;
    Color changeColor;

	// Use this for initialization
	override protected void Start () {
        base.Start();
        HP = 50;
        moveSpeed = 0.3f;
        endExplotion = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (HP <= 10 && !isSelfDestruct) {
            startSelfDestruct = true;
            isSelfDestruct = true;
        }
        if (startSelfDestruct && !endExplotion) {
            selfDestruct();
        }
	}

    protected override void FixedUpdate() {
        
    }

    protected override void Attack() {
        
    }

    void bodyBlow() {

    }

    void laserAttack() {

    }

    void hormingBullet() {

    }

    void selfDestruct() {
        selfDestructCount += Time.deltaTime;
        GetComponent<Renderer>().material.color = new Color(1,1 - (1 * selfDestructCount / selfDestructTime), 0,1);
        if (selfDestructCount >= selfDestructTime) {
            GameObject exp = Instantiate(detonator, transform.position, Quaternion.identity);
            Destroy(exp, 5f);
            endExplotion = true;
        }
    }
}
