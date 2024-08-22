using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpManager : MonoBehaviour
{
    private PlayerXp playerXp;
    private Shoot shotBullet;
    private ShootBeam shotBeam;
    private ShootWave shotWave;
    private RhythmGameController RGC;
    private PlayerHp playerHp;
    private PlayerMove playerMove;
    private LevelUpUpgradesSelect levelUpUpgradesSelect;
    // Start is called before the first frame update
    void Start()
    {
        playerXp = GameObject.Find("Hexagon").GetComponent<PlayerXp>();
        shotBullet = GameObject.Find("ShotWeapon").GetComponent<Shoot>();
        shotBeam = GameObject.Find("ShotBeam").GetComponent<ShootBeam>();
        shotWave = GameObject.Find("ShotWave").GetComponent<ShootWave>();
        RGC = GameObject.Find("RhythmBar").GetComponent<RhythmGameController>();
        playerHp = GameObject.Find("Hexagon").GetComponent<PlayerHp>();
        playerMove = GameObject.Find("Hexagon").GetComponent<PlayerMove>();
        levelUpUpgradesSelect = GameObject.Find("GameControl").GetComponent<LevelUpUpgradesSelect>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerXp.activated)
        {
            StartCoroutine(c_waitForInteractable());
        }
    }

    public void upgradeBullet()
    {
        shotBullet.increaseBulletDmg();

        playerXp.closeUpgradeTab();
    }
    public void upgradeBeam()
    {
        shotBeam.beamDmg += 3;
        shotBeam.beamNumber++;

        playerXp.closeUpgradeTab();
    }
    public void upgradeWave()
    {
        shotWave.waveDamage += 4;
        shotWave.finalScale = new Vector3(shotWave.finalScale.x * 1.1f, shotWave.finalScale.y * 1.1f, 1f);

        playerXp.closeUpgradeTab();
    }

    public void upgradePenalty()
    {
        RGC.penaltyTime -= 0.1f;
        if(RGC.penaltyTime <= 0.5f)
        {
            levelUpUpgradesSelect.maxPenaltyUpgraded();
        }
        playerXp.closeUpgradeTab();
    }

    public void upgradeMaxHp()
    {
        playerHp.maxHp += 10;
        playerHp.HpRegen++;
        playerXp.closeUpgradeTab();
    }

    public void upgradePlayerMove()
    {
        playerMove.speed += 0.5f;

        playerXp.closeUpgradeTab();
    }

    IEnumerator c_waitForInteractable()
    {
        gameObject.GetComponent<Button>().interactable = false;
        yield return new WaitForSecondsRealtime(0.5f);
        gameObject.GetComponent<Button>().interactable = true;
        playerXp.activated = false;
    }
}
