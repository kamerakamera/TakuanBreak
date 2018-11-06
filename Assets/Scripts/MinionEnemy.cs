using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionEnemy : Enemy {
    [SerializeField]
    GameObject explodePrefab;
    // Use this for initialization

    // Update is called once per frame
    protected override void Start() {
        base.Start();
        moveSpeed = 5.0f;
    }

    void Update () {
		
	}

    protected override void FixedUpdate() {
        base.FixedUpdate();
    }

    protected override void Death() {
        takuanSoundEffect.clip = takuanDamegeSoundEffect;
        takuanSoundEffect.Play();
        DestroyObject();
    }

    private void OnCollisionEnter(Collision collision) {
        if(collision.collider.tag == "Player") {
            Instantiate(explodePrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

}
