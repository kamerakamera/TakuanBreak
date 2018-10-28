using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManeger : MonoBehaviour {
    [SerializeField]
    Slider playerHealth, bossHealth;
    [SerializeField]
    Player player;
    float playerStartHp,bossStartHp;
    [SerializeField]
    StageManeger stageManeger;
    [SerializeField]
    GameObject bossHpBarObj;
    Boss boss;
    bool isDisplayBossHealth;

	// Use this for initialization
	void Start () {
        playerStartHp = player.startHp;
        isDisplayBossHealth = false;
        bossHpBarObj.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        DisplayPlayerHealth();
        if (stageManeger.isBoss && !isDisplayBossHealth) {
            GetBossComponent();
            bossHpBarObj.SetActive(true);
        }

        if(!stageManeger.isBoss && isDisplayBossHealth) {
            isDisplayBossHealth = false;
            bossHpBarObj.SetActive(false);
        }

        if(stageManeger.isBoss && isDisplayBossHealth) {
            DisplayBossHealth();
        }
	}

    void DisplayPlayerHealth() {
        playerHealth.value = player.Hp/playerStartHp;
    }

    void DisplayBossHealth() {
        bossHealth.value = boss.HP/bossStartHp;
    }

    void GetBossComponent() {
        boss = stageManeger.nowBossTakuan.GetComponent<Boss>();
        bossStartHp = boss.HP;
        isDisplayBossHealth = true;
    }
}
