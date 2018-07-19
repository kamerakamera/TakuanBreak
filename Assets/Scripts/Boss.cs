using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy {

	// Use this for initialization
	override protected void Start () {
        base.Start();
        HP = 30;
        moveSpeed = 0.3f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    protected override void FixedUpdate() {
        
    }

    protected override void Attack() {
        
    }
}
