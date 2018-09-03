using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy {
    public GameObject detonator,selfDestructEffect;
    public float selfDestructHP = 10;
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
        if (HP <= selfDestructHP && !isSelfDestruct) {
            StartDestruct();
        }
        if (startSelfDestruct && !endExplotion) {
            SelfDestruct();
        }
	}

    protected override void FixedUpdate() {
        
    }

    protected override void Attack() {
        
    }

    protected override void Death() {
        takuanSoundEffect.clip = takuanDamegeSoundEffect;
        takuanSoundEffect.Play();
        Destroy(this.gameObject);
        Instantiate(takuanDiedParticle, transform.position, Quaternion.identity);
    }

    void BodyBlow() {

    }

    void LaserAttack() {

    }

    void HormingBullet() {

    }

    void StartDestruct() {
        startSelfDestruct = true;
        isSelfDestruct = true;
    }

    void SelfDestruct() {
        selfDestructCount += Time.deltaTime;
        GetComponent<Renderer>().material.color = new Color(1,1 - (1 * selfDestructCount / selfDestructTime), 0,1);
        if (selfDestructCount >= selfDestructTime) {
            GameObject exp = Instantiate(detonator, transform.position, Quaternion.identity);
            Destroy(exp, 5f);
            endExplotion = true;
        }
    }
}
