using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowSpeedField : MonoBehaviour {
    float beforePlayerMovePow, deleteCount, deleteTime = 5;
    Color startColor;
    static bool isDebuff;
    private bool isTouch;
    GameObject player;
    

	// Use this for initialization
	void Start () {
        startColor = GetComponent<Renderer>().material.color;

    }
	
	// Update is called once per frame
	void Update () {
        DeleteCount();
        if (isDebuff && isTouch) {
            SetLowSpeed();
        }
	}

    void DeleteCount() {
        deleteCount += Time.deltaTime;
        ChengeColor(deleteCount,deleteTime);
        if (deleteCount >= deleteTime) {
            if(isDebuff) {
                OnTriggerExit(player.GetComponent<Collider>());
            }
            Destroy(this.gameObject);
        }
    }

    void ChengeColor(float delta,float max) {
        GetComponent<Renderer>().material.color = new Color(startColor.r, startColor.g, startColor.b, 1 - delta / max);
    }

    void SetLowSpeed() {
        player.GetComponent<Player>().MovePower = 1;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player") {
            player = other.gameObject;
            beforePlayerMovePow = player.GetComponent<Player>().StartMovePower;
            isDebuff = true;
            isTouch = true;
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.tag == "Player") {
            isDebuff = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Player") {
            player.GetComponent<Player>().MovePower = beforePlayerMovePow;
            isDebuff = false;
            isTouch = false;
        }
    }

}
