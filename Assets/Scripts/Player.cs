using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
    Rigidbody rb;
    public int Hp { get; set; }
    public int startHp;
    [SerializeField]
    private GameObject Bullet,homingBulletPrefab;
    private GameObject isShotHomingBullet;
    public Camera mainCamera;
    public Vector3 mousePosition;
    float changeAmountAxisY;
    public static float rotateSensitivityPower = 4;
    [SerializeField]
    float avoidInterval,avoidCoolTime;
    float knockBackTime = 0,invincibleCoolTime = 0,avoidIntervalCount,avoidCoolTimeCount,lockOnCount,lockOnTime = 1,deathCount;
    public float lockOnShotCoolTime, lockOnShotCoolTimeCount;
    RaycastHit hit;
    bool isKnockBack, isAvoid, avoidAble, lockOnShotAble, avoidInput, lockOnInput, startLockOn, isLaunchPreparation, completeLockOn;
    Vector3 moveDirection;
    bool front,back,right,left,up;
    int straight, side,lockOnTargetRemoveCount;
    public bool isInvincible,isCreateTargetSite,isDeath;
    public float MovePower { get; set; }
    public float StartMovePower { get; set; }
    public Image damegeFlash;
    AudioSource soundEffect;
    public AudioClip shotSoundEffect;
    public AudioClip damegeSoundEffect;
    public AudioClip lockOnShotSound;
    public List<GameObject> lockOnTargetObj;
    [SerializeField]
    UIManeger uiManeger;
    [SerializeField]
    GameObject LockOntargetView;
    

    // Use this for initialization
    void Start () {
        Hp = startHp;
        rb = GetComponent<Rigidbody>();
        soundEffect = GetComponent<AudioSource>();
        MovePower = 3;
        StartMovePower = MovePower;
        damegeFlash.enabled = false;
        avoidAble = true;
        isAvoid = false;
        isCreateTargetSite = false;
        lockOnTargetRemoveCount = 0;
        isLaunchPreparation = false;
        startLockOn = false;
        LockOntargetView.SetActive(false);
        isDeath =false;
	}
	
	// Update is called once per frame
	void Update () {
        if (isDeath) {
            Death();
        }
        if (!isDeath) {
            if (Input.GetMouseButtonDown(0) && !isLaunchPreparation) {
                Shot();
            }
            MoveInput();
            AvoidInput();
            if (lockOnShotAble) {
                LockOnInput();
            }
        }
        
    }

    void FixedUpdate() {
        if (!lockOnShotAble) {
            IntervalCount(ref lockOnShotCoolTimeCount, lockOnShotCoolTime, ref lockOnShotAble, true);
        }
        if (!isDeath) {
            LookAround();
        }
        if (!isLaunchPreparation) {
            if (avoidInput && avoidAble && !isAvoid) {
                Avoid();
                isAvoid = true;
                avoidAble = false;
            }
        }
        if (!isAvoid){
            Move();
            if (lockOnShotAble) {
                if (lockOnInput && startLockOn && isLaunchPreparation && !isCreateTargetSite) {
                    LockOnTarget();
                    LockOntargetView.SetActive(true);
                    uiManeger.CreateTargetSite(lockOnTargetObj, lockOnTargetObj.Count);
                    isCreateTargetSite = true;
                }
                if (isCreateTargetSite) {
                    uiManeger.TrackingTarget(lockOnTargetObj,ref isCreateTargetSite);
                    if (!isCreateTargetSite) {
                        ResetLockOnBool();
                    }
                }
                if (isLaunchPreparation && completeLockOn) {
                    if (lockOnTargetObj.Count >= 1) {
                        for (int i = 0; i < lockOnTargetObj.Count; i++) {
                            ShotHomingBullet(i);
                        }
                    }
                    ResetLockOnBool();
                }
            }
            
        }
        if (isAvoid) {
            IntervalCount(ref avoidIntervalCount, avoidInterval, ref isAvoid, false);
        }
        if(!isAvoid && !avoidAble) {
            IntervalCount(ref avoidCoolTimeCount, avoidCoolTime, ref avoidAble, true);
        }

        //無敵時間
        if (isInvincible) {
            invincibleCoolTime += Time.fixedDeltaTime;
            damegeFlash.color = new Color(damegeFlash.color.r, damegeFlash.color.g, damegeFlash.color.b, 1 - invincibleCoolTime);
            if (invincibleCoolTime >= 1.0f) {
                isInvincible = false;
            }
        }

        //ノックバック処理
        if (isKnockBack) {
            knockBackTime += Time.fixedDeltaTime;
            if (knockBackTime >= 0.8f) {
                isKnockBack = false;
            }
        }
    }

    void LookAround() {
        mousePosition = Input.mousePosition;
        float mousePositionX = Input.GetAxis("Mouse X");
        float mousePositionY = Input.GetAxis("Mouse Y");
        //縦回転
        if (transform.localEulerAngles.x - mousePositionY <= 290 && transform.localEulerAngles.x - mousePositionY >= 180) {
            transform.Rotate(290 - transform.localEulerAngles.x, 0, 0);
        } else if (transform.localEulerAngles.x - mousePositionY >= 70 && transform.localEulerAngles.x - mousePositionY <= 180) {
            transform.Rotate(70 - transform.localEulerAngles.x, 0, 0);
        } else transform.Rotate(-mousePositionY * rotateSensitivityPower, 0, 0);
        //横回転
        transform.Rotate(0, mousePositionX * rotateSensitivityPower, 0, Space.World);
    }

    void MoveInput() {
        if (Input.GetKey("w")) {
            front = true;
        } else front = false;

        if (Input.GetKey("s")) {
            back = true;
        } else back = false;

        if (Input.GetKey("a")) {
            left = true;
        } else left = false;

        if (Input.GetKey("d")) {
            right = true;
        } else right = false;
        if (front) {
            straight = 1;
        }
        if (back) {
            straight = -1;
        }
        if ((front && back) || (!front && !back)) straight = 0;

        if (left) {
            side = -1;
        }
        if (right) {
            side = 1;
        }
        if ((left && right) || (!left && !right)) side = 0;
    }

    void Move() {
        if (!isKnockBack) {
            moveDirection = new Vector3(transform.forward.x * straight + transform.right.x * side,rb.velocity.y,transform.forward.z * straight + transform.right.z * side).normalized;
            rb.velocity = new Vector3(moveDirection.x * MovePower, rb.velocity.y, moveDirection.z * MovePower);
        }
    }

    void AvoidInput() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            avoidInput = true;
        } else {
            avoidInput = false;
        }
    }

    void Avoid() {
        //回避方法は色々考えた方がよさそう
        if (side == 0 && straight == 0) {
            straight = -1;
        }
        moveDirection = new Vector3(transform.forward.x * straight + transform.right.x * side, rb.velocity.y, transform.forward.z * straight + transform.right.z * side).normalized;
        rb.velocity = new Vector3(moveDirection.x * MovePower * 8.0f, rb.velocity.y, moveDirection.z * MovePower * 8.0f);
    }

    void IntervalCount(ref float count, float intervalTime, ref bool isTrigger, bool setBool) {
        count += Time.deltaTime;
        if (count >= intervalTime) {
            isTrigger = setBool;
            count = 0;
        }
    }

    void Shot() {
        /*if (Physics.Raycast(transform.position, transform.forward * 100, out hit,Mathf.Infinity)) {
            soundEffect.clip = shotSoundEffect;
            soundEffect.Play();
            if(hit.collider.tag == "Takuan") {
                hit.collider.GetComponent<Enemy>().Damege();
            }
        }*/
        soundEffect.clip = shotSoundEffect;
        soundEffect.Play();
        Instantiate(Bullet,transform.position,transform.rotation);
    }

    void LockOnInput() {
        //一定時間入力させ続ける
        if (Input.GetMouseButtonDown(1) && !isLaunchPreparation) {
            lockOnInput = true;
            isLaunchPreparation = true;
            startLockOn = true;
        } else {
            lockOnInput = false;
        }
        if (startLockOn) {
            lockOnCount += Time.deltaTime;
            if(lockOnCount >= lockOnTime) {
                lockOnCount = 0;
                startLockOn = false;
                completeLockOn = true;
            }
        }
    }

    void ResetLockOnBool() {
        LockOntargetView.SetActive(false);
        lockOnShotAble = false;
        uiManeger.DestroyTargetSite();
        isCreateTargetSite = false;
        isLaunchPreparation = false;
        completeLockOn = false;
        startLockOn = false;
        lockOnCount = 0;
        lockOnTargetObj.Clear();
    }

    void LockOnTarget() {
        soundEffect.clip = lockOnShotSound;
        soundEffect.Play();
        foreach (GameObject checkObj in GameObject.FindGameObjectsWithTag("Takuan")) {
            if (checkObj.GetComponent<Enemy>().IsRendered) {
                lockOnTargetObj.Add(checkObj);
            }
        }
        foreach (GameObject checkObj in GameObject.FindGameObjectsWithTag("Boss")) {
            if (checkObj.GetComponent<Boss>().IsRendered) {
                lockOnTargetObj.Add(checkObj);
            }
        }
    }

    void ShotHomingBullet(int bulletNum) {
        soundEffect.clip = shotSoundEffect;
        soundEffect.Play();
        isShotHomingBullet = Instantiate(homingBulletPrefab,transform.position,Quaternion.identity);
        if (lockOnTargetObj[bulletNum] != null) {
            isShotHomingBullet.GetComponent<HomingBullet>().SetTargetEnemy(lockOnTargetObj[bulletNum]);
        }
    }

    public void Damege() {
        if (!isInvincible) {
            Hp -= 1;
            soundEffect.clip = damegeSoundEffect;
            soundEffect.Play();
            isKnockBack = true;
            isInvincible = true;
            damegeFlash.enabled = true;
            knockBackTime = 0;
            invincibleCoolTime = 0;
            if (isLaunchPreparation) {
                ResetLockOnBool();
            }
            if (Hp <= 0) {
                isDeath = true;
            }
        }
    }

    public void Damege(int damege) {
        Hp -= damege - 1;
        Damege();
    }

    void Death() {
        //SceneManager.LoadScene("End");
        //カメラ吹き飛ばし処理して時間経過でSceneとばそうぜ
        mainCamera.transform.parent = null;
        mainCamera.transform.position += new Vector3(0,1.0f * Time.deltaTime,0);
        deathCount += Time.deltaTime;
        if(deathCount >= 3) {
            SceneManager.LoadScene("GameOver");
        }
    }
}
