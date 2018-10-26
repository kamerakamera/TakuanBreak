using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
    Rigidbody rb;
    public int Hp { get; set; }
    public int startHp;
    public GameObject Bullet;
    public Camera mainCamera;
    public Vector3 mousePosition;
    float changeAmountAxisY;
    public static float rotateSensitivityPower = 4;
    float jumpCoolTime = 0,knockBackTime = 0,invincibleCoolTime = 0;
    RaycastHit hit;
    bool isJump,isKnockBack;
    Vector3 moveDirection;
    bool front,back,right,left,up;
    int straight, side;
    public bool isInvincible;
    float movePower;
    public Image damegeFlash;
    AudioSource soundEffect;
    public AudioClip shotSoundEffect;
    public AudioClip damegeSoundEffect;
    

    // Use this for initialization
    void Start () {
        Hp = startHp;
        rb = GetComponent<Rigidbody>();
        soundEffect = GetComponent<AudioSource>();
        movePower = 3;
        damegeFlash.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0)) {
            Shot();
        }
        MoveInput();
    }

    void FixedUpdate() {
        Move();
        lookAround();

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

    void lookAround() {
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

        if (Input.GetKeyDown(KeyCode.Space) && up == false) {
            up = true;
        } else up = false;

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
            rb.velocity = new Vector3(moveDirection.x * movePower, rb.velocity.y, moveDirection.z * movePower);

            if (up && isJump == false) {
                rb.velocity = new Vector3(rb.velocity.x, 10f, rb.velocity.z);
                isJump = true;
            }
            if (isJump) {
                jumpCoolTime += Time.fixedDeltaTime;
                if (jumpCoolTime >= 10) {
                    jumpCoolTime = 0;
                    isJump = false;
                }
            }
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
            if (Hp <= 0) {
                Death();
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
    }

}
