using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum BossState {
    wait,shotBullet,shotCreateFieldBullet,destruct,summons,bodyRush,backStartPos
}

public class Boss : Enemy {
    public GameObject detonator,selfDestructEffect;
    public float selfDestructHP = 10;
    private float selfDestructTime = 7,selfDestructCount;
    Material takuanMaterial;
    Color changeColor;
    Vector3 playerDirection;
    float playerDistance;
    [SerializeField]
    GameObject bulletPrefab,destructParticlePrefab,createFieldBulletPrefab,lowSpeedFieldPrefab,minionPrefab;
    GameObject destructParticle;
    BossState IsState { get; set; }
    [SerializeField]
    float remainingBullet,shotIntervalTime,remainingCreateFieldBullet,createNum;
    [SerializeField]
    float shotSpeed, bodyRushSpeed,waitInterval,createMinionInterval;
    float shotCount,shotIntervalCount,waitIntervalCount,createMinionIntervalCount;
    bool isShot,isStartDestruct,isDestruct,isWait,isAvoid,isMinionCreate;
    Vector3 firstPosition;

    // Use this for initialization
    override protected void Start () {
        base.Start();
        firstPosition = transform.position;
        moveSpeed = 0.3f;
        isDestruct = false;
        isStartDestruct = false;
        stageManeger = GameObject.Find("StageManeger").GetComponent<StageManeger>();
        ChengeState(BossState.wait);
	}
	
	// Update is called once per frame
	void Update () {
        UpdateAction();
	}

    void ChengeState(BossState stateName) {
        IsState = stateName;
        if(stateName == BossState.wait) {
            GetPlayerDistance();
            isWait = true;
        }
        if(stateName == BossState.shotBullet) {
            shotCount = 0;
            isShot = false;
        }
        if(stateName == BossState.shotCreateFieldBullet) {
            shotCount = 0;
            isShot = false;
        }
        if(stateName == BossState.bodyRush) {
            GetPlayerDirection();
        }
    }

    void UpdateAction() {
        if(IsState == BossState.wait) {
            //State変更処理
            PlayerLook();
            if (isWait) {
                IntervalCount(ref waitIntervalCount, waitInterval, ref isWait, false);
            }else{
                if (isStartDestruct == true) {
                    destructParticle = Instantiate(destructParticlePrefab, Vector3.zero, Quaternion.identity);
                    isStartDestruct = false;
                    isDestruct = true;
                    ChengeState(BossState.destruct);
                } else {
                    if (playerDistance <= 6) {
                        ChengeState(BossState.bodyRush);
                    } else {
                        if(Random.Range(1,4) == 1) {
                            ChengeState(BossState.shotCreateFieldBullet);
                        } else {
                            if (Random.Range(1, 5) == 1) {
                                ChengeState(BossState.summons);
                            } else {
                                ChengeState(BossState.shotBullet);
                            }
                        }
                    }
                }
            }
        }
        if(IsState == BossState.shotBullet) {
            //一定時間ごとにshotする処理
            PlayerLook();
            if(isShot == false) {
                ShotExplodeBullet();
                shotCount++;
            } else {
                IntervalCount(ref shotIntervalCount, shotIntervalTime,ref isShot, false);
                if (shotCount >= remainingBullet) {
                    ChengeState(BossState.wait);
                }
            }
        }
        if(IsState == BossState.shotCreateFieldBullet) {
            PlayerLook();
            if (isShot == false) {
                ShotDebuffBullet();
                shotCount++;
            } else {
                IntervalCount(ref shotIntervalCount, shotIntervalTime, ref isShot, false);
                if (shotCount >= remainingCreateFieldBullet) {
                    ChengeState(BossState.wait);
                }
            }
        }
        if(IsState == BossState.summons) {
            //雑魚召喚
            CreateMinions();
        }
        if(IsState == BossState.destruct) {
            SelfDestruct();
            Spin();
        }
        if(IsState == BossState.bodyRush) {
            BodyBlow();
        }
        if(IsState == BossState.backStartPos) {
            PlayerLook();
            rb.velocity = (firstPosition - transform.position).normalized * bodyRushSpeed * 0.6f;
            if(Vector3.Distance(transform.position,firstPosition) <= 0.2f) {
                transform.position = firstPosition;
                rb.velocity = Vector3.zero;
                ChengeState(BossState.wait);
                if (HP <= selfDestructHP && !isDestruct) {
                    isStartDestruct = true;
                }
            }
        }
    }

    protected override void FixedUpdate() {
        
    }

    protected override void Attack() {
        
    }

    protected override void Death() {
        takuanSoundEffect.clip = takuanDamegeSoundEffect;
        takuanSoundEffect.Play();
        if(destructParticle != null) {
            Destroy(destructParticle);
        }
        Destroy(this.gameObject);
        Instantiate(takuanDiedParticle, transform.position, Quaternion.identity);
    }

    void PlayerLook() {
        transform.rotation = Quaternion.LookRotation(player.transform.position);
    }

    void BodyBlow() {
        rb.velocity = playerDirection * bodyRushSpeed;
        Spin();
    }

    void CreateMinions() {
        Spin();
        IntervalCount(ref createMinionInterval,createMinionIntervalCount,ref isMinionCreate,true);
        if (isMinionCreate) {
            for (int i = 0; i < createNum; i ++) {
                Instantiate(minionPrefab, transform.position + new Vector3(Random.Range(-4,4),-3, Random.Range(-4, 4)), Quaternion.identity);
            }
            isMinionCreate = false;
            ChengeState(BossState.wait);
        }
    }

    void ShotDebuffBullet() {
        GetPlayerDirection();
        GameObject debuffBullet = Instantiate(createFieldBulletPrefab, transform.position + playerDirection * 2.0f, Quaternion.identity);
        debuffBullet.GetComponent<Rigidbody>().velocity = playerDirection * shotSpeed;
        debuffBullet.GetComponent<CreateFieldBullet>().SetFieldPrefab(lowSpeedFieldPrefab);
        isShot = true;
    }

    void ShotExplodeBullet() {
        GetPlayerDirection();
        GameObject explodeBullet = Instantiate(bulletPrefab, transform.position + playerDirection * 2.0f, Quaternion.identity);
        explodeBullet.GetComponent<Rigidbody>().velocity = playerDirection * shotSpeed;
        isShot = true;
    }

    void GetPlayerDirection() {
        playerDirection = (player.transform.position - transform.position).normalized;
    }

    void GetPlayerDistance() {
        playerDistance = Vector3.Distance(transform.position, player.transform.position);
    }

    void SelfDestruct() {
        selfDestructCount += Time.deltaTime;
        GetComponent<Renderer>().material.color = new Color(1,1 - (1 * selfDestructCount / selfDestructTime), 0,1);
        if (selfDestructCount >= selfDestructTime) {
            GameObject exp = Instantiate(detonator, transform.position, Quaternion.identity);
            Destroy(exp, 5f);
            isDestruct = false;
            ChengeState(BossState.wait);
            Destroy(destructParticle);
            Death();
        }
    }

    public override void Damege() {
        base.Damege();
        if(HP <= selfDestructHP && !isDestruct) {
            ChengeState(BossState.backStartPos);
        }
    }

    void IntervalCount(ref float count,float intervalTime,ref bool isTrigger,bool setBool) {
        count += Time.deltaTime;
        if(count >= intervalTime) {
            isTrigger = setBool;
            count = 0;
        }
    }

    protected override void OnCollisionStay(Collision collision) {
        if (collision.collider.tag == "Player") {
            Player p = player.GetComponent<Player>();
            Rigidbody playerRb = player.GetComponent<Rigidbody>();
            if (!p.isInvincible) {
                playerRb.velocity = new Vector3(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y, player.transform.position.z - transform.position.z).normalized * 10f;
            }
            p.Damege(8);
        }
        if (IsState == BossState.bodyRush) {
            ChengeState(BossState.backStartPos);
        }
    }
}
