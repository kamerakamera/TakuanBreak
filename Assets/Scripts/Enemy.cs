using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public GameObject player;
    protected Rigidbody rb;
    public float HP;
    protected float moveSpeed = 1;
    public static int hard;
    public GameObject takuanDiedParticle;
    public GameObject takuanDamegeParticle;
    protected AudioSource takuanSoundEffect;
    public AudioClip takuanDamegeSoundEffect;
    public StageManeger stageManeger;
    public bool IsRendered { get; set; }
	// Use this for initialization
	protected virtual void Start () {
        rb = GetComponent<Rigidbody>();
        stageManeger = GameObject.Find("StageManeger").GetComponent<StageManeger>();
        player = GameObject.Find("Player");
        takuanSoundEffect = GetComponent<AudioSource>();
        moveSpeed = 1;
        if (hard >= 1) {
            moveSpeed += hard * 0.1f;
        }
        //Debug.Log(moveSpeed);
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    protected virtual void FixedUpdate() {
        Spin();
        Attack();
    }

    protected void Spin() {
        transform.Rotate(0, 80, 0);
    }

    protected virtual void Attack() {
        Vector3 playerPosition = GameObject.Find("Player").transform.position;
        rb.velocity = (playerPosition - transform.position).normalized * moveSpeed;
    }

    public virtual void Damege() {
        HP -= 1;
        if(HP <= 0) {
            Death();
        }
        else {
            Instantiate(takuanDamegeParticle, transform.position, Quaternion.identity);
        }
    }
    public void Damege(int damege) {
        HP -= damege;
        if (HP <= 0) {
            Death();
        } else {
            for(int i = 0;i < damege; i++) {
                Instantiate(takuanDamegeParticle, transform.position, Quaternion.identity);
            }
        }
    }

    protected virtual void Death() {
        takuanSoundEffect.clip = takuanDamegeSoundEffect;
        takuanSoundEffect.Play();
        DestroyObject();
        stageManeger.AddScore();
        if (stageManeger.GetScore() % 100.0f == 0) {
            stageManeger.StartBossStage();
        } else {
            stageManeger.Search();
        }
    }

    public void DestroyObject() {
        Destroy(this.gameObject);
        Instantiate(takuanDiedParticle, transform.position, Quaternion.identity);
    }

    private void OnBecameVisible() {
        IsRendered = true;
    }

    private void OnBecameInvisible() {
        IsRendered = false;
    }

    protected virtual void OnCollisionStay(Collision collision) {
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
