using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
    Rigidbody rb;
    private int HP = 3;
    public int startHP;
    public Camera mainCamera;
    public Vector3 mousePosition;
    float changeAmountAxisY;
    public static float rotateSensitivityPower = 4;
    float jumpCoolTime = 0,knockBackTime = 0,invincibleCoolTime = 0;
    RaycastHit hit;
    bool isJump,isKnockBack;
    public bool isInvincible;
    float movePower;
    public Image damegeFlash;
    AudioSource soundEffect;
    public AudioClip shotSoundEffect;
    public AudioClip damegeSoundEffect;
    

    // Use this for initialization
    void Start () {
        HP = startHP;
        rb = GetComponent<Rigidbody>();
        soundEffect = GetComponent<AudioSource>();
        movePower = 3;
        damegeFlash.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        
        
    }

    void FixedUpdate() {
        Move();
        lookAround();
        if (Input.GetMouseButtonDown(0)) {
            Shot();
        }
        if (isInvincible) {
            invincibleCoolTime += Time.fixedDeltaTime;
            damegeFlash.color = new Color(damegeFlash.color.r, damegeFlash.color.g, damegeFlash.color.b, 1 - invincibleCoolTime);
            if (invincibleCoolTime >= 5.0f) {
                isInvincible = false;
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

    void Move() {
        if (!isKnockBack) {
            if (Input.GetKey("w")) {
                rb.velocity = new Vector3(transform.forward.x * movePower, rb.velocity.y, transform.forward.z * movePower);
            }
            if (Input.GetKey("s")) {
                rb.velocity = new Vector3(transform.forward.x * -movePower, rb.velocity.y, transform.forward.z * -movePower);
            }
            if (Input.GetKey("a")) {
                rb.velocity = new Vector3(transform.right.x * -movePower, rb.velocity.y, transform.right.z * -movePower);
            }
            if (Input.GetKey("d")) {
                rb.velocity = new Vector3(transform.right.x * movePower, rb.velocity.y, transform.right.z * movePower);
            }
            if (Input.GetKeyDown(KeyCode.Space) && isJump == false) {
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
        if (isKnockBack) {
            knockBackTime += Time.fixedDeltaTime;
            if(knockBackTime >= 0.8f) {
                isKnockBack = false;
            }
        }
    }

    void Shot() {
        if (Physics.Raycast(transform.position, transform.forward * 100, out hit,Mathf.Infinity)) {
            soundEffect.clip = shotSoundEffect;
            soundEffect.Play();
            if(hit.collider.tag == "Takuan") {
                hit.collider.GetComponent<Enemy>().Damege();

            }
        }
    }

    public void Damege() {
        if (!isInvincible) {
            HP -= 1;
            soundEffect.clip = damegeSoundEffect;
            soundEffect.Play();
            isKnockBack = true;
            isInvincible = true;
            damegeFlash.enabled = true;
            knockBackTime = 0;
            invincibleCoolTime = 0;
            if (HP <= 0) {
                Death();
            }
        }
    }

    void Death() {
        SceneManager.LoadScene("End");
    }

    public int DisplyHealth() {
        return HP;
    } 
}
