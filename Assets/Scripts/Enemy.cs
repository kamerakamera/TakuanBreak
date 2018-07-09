﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public GameObject player;
    Rigidbody rb;
    float HP = 3;
    float moveSpeed = 1;
    public static int hard;
    public GameObject takuanDiedParticle;
    public GameObject takuanDamegeParticle;
    AudioSource takuanSoundEffect;
    public AudioClip takuanDamegeSoundEffect;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        takuanSoundEffect = GetComponent<AudioSource>();
        HP = 3;
        moveSpeed = 1;
        if (hard >= 1) {
            moveSpeed += hard * 0.1f;
        }
        //Debug.Log(moveSpeed);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate() {
        Spin();
        Attack();
    }

    void Spin() {
        transform.Rotate(0, 0, 80);
    }

    void Attack() {
        Vector3 playerPosition = GameObject.Find("Player").transform.position;
        rb.velocity = (playerPosition - transform.position).normalized * moveSpeed;
    }

    public void Damege() {
        HP -= 1;
        if(HP <= 0) {
            Death();
        }
        else {
            Instantiate(takuanDamegeParticle, transform.position, Quaternion.identity);
        }
    }

    void Death() {
        takuanSoundEffect.clip = takuanDamegeSoundEffect;
        takuanSoundEffect.Play();
        GameObject.Find("StageManeger").GetComponent<StageManeger>().AddScore();
        Destroy(this.gameObject);
        GameObject.Find("StageManeger").GetComponent<StageManeger>().Search();
        Instantiate(takuanDiedParticle, transform.position, Quaternion.identity);
    }

    private void OnCollisionStay(Collision collision) {
        if (collision.collider.tag == "Player") {
            Player p = player.GetComponent<Player>();
            Rigidbody playerRb = player.GetComponent<Rigidbody>();
            if (!p.isInvincible) {
                playerRb.velocity = new Vector3(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y, player.transform.position.z - transform.position.z).normalized * 10f;
            }
            p.Damege();
        }
    }
}
