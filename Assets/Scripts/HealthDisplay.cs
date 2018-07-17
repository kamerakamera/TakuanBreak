using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour {
    public Text healthText;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        UpdateHealthDisplay();
    }

    public void UpdateHealthDisplay() {
        healthText.text = "HP " + GameObject.Find("Player").GetComponent<Player>().Hp.ToString();
    }
}
