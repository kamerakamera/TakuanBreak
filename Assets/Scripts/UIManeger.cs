using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManeger : MonoBehaviour {
    [SerializeField]
    Slider playerHealth, bossHealth,playerHomingShotCoolTime;
    [SerializeField]
    Player player;
    float playerStartHp,bossStartHp;
    [SerializeField]
    StageManeger stageManeger;
    [SerializeField]
    GameObject bossHpBarObj;
    Boss boss;
    bool isDisplayBossHealth;
    [SerializeField]
    Image skillBarImage;
    List<GameObject> isTargetObj = new List<GameObject>();
    [SerializeField]
    GameObject canvas,targetSitePrefab,playerUI;
    Color chergeTimeColor = new Color(246.0f / 255.0f, 255.0f / 255.0f, 0.0f / 255.0f, 150.0f / 255.0f),maxColor = new Color(80.0f / 255.0f, 255.0f / 255.0f, 0.0f / 255.0f, 230.0f / 255.0f);

	// Use this for initialization
	void Start () {
        playerStartHp = player.startHp;
        isDisplayBossHealth = false;
        bossHpBarObj.SetActive(false);
        playerUI.SetActive(true);
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
        DisplayerSkillCoolTime();
	}

    void DisplayPlayerHealth() {
        playerHealth.value = player.Hp/playerStartHp;
    }

    void DisplayerSkillCoolTime() {
        if(player.lockOnShotCoolTimeCount == 0) {
            playerHomingShotCoolTime.value = 1;
            skillBarImage.color = maxColor;
        } else {
            playerHomingShotCoolTime.value = player.lockOnShotCoolTimeCount / player.lockOnShotCoolTime;
            skillBarImage.color = chergeTimeColor;
        }
    }

    void DisplayBossHealth() {
        bossHealth.value = boss.HP/bossStartHp;
    }

    void GetBossComponent() {
        boss = stageManeger.nowBossTakuan.GetComponent<Boss>();
        bossStartHp = boss.HP;
        isDisplayBossHealth = true;
    }

    public void CreateTargetSite(List<GameObject> targetPos,int num) {
        playerUI.SetActive(false);
        for (int i = 0; i < num; i++) {
            isTargetObj.Add(Instantiate(targetSitePrefab));
            isTargetObj[i].transform.SetParent(canvas.transform);
            isTargetObj[i].GetComponent<RectTransform>().position = RectTransformUtility.WorldToScreenPoint(Camera.main, targetPos[i].transform.position);
        }
    }

    public void TrackingTarget(List<GameObject> target,ref bool _isTrack) {
        if (target.Count == 0) {
            _isTrack = false;
            return;
        }
        for (int i = 0; i < target.Count; i++) {
            if(target[i].tag == "Takuan") {
                if (!target[i].GetComponent<Enemy>().IsRendered) {
                    target.RemoveAt(i);
                    Destroy(isTargetObj[i]);
                    isTargetObj.RemoveAt(i);
                    continue;
                }
            }
            if (target[i].tag == "Boss") {
                if (!target[i].GetComponent<Boss>().IsRendered) {
                    target.RemoveAt(i);
                    Destroy(isTargetObj[i]);
                    isTargetObj.RemoveAt(i);
                    continue;
                }
            }
            isTargetObj[i].GetComponent<RectTransform>().position = RectTransformUtility.WorldToScreenPoint(Camera.main, target[i].transform.position);
        }
    }

    public void DestroyTargetSite() {
        foreach(GameObject destroyObj in isTargetObj) {
            Destroy(destroyObj);
        }
        isTargetObj = new List<GameObject>();
        playerUI.SetActive(true);
    }
}
