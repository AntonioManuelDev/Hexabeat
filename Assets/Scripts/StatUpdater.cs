using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatUpdater : MonoBehaviour
{
    public TextMeshProUGUI Hp, MS, BulletDamage, LaserDamage, LaserCount, WaveDamage, WaveArea, Penalty;
    PlayerHp playerHp;
    PlayerMove playerMove;
    Shoot shotBullet;
    ShootBeam shotBeam;
    ShootWave shotWave;
    RhythmGameController rhythmGameController;
    // Start is called before the first frame update
    void Start()
    {
        playerHp = GameObject.Find("Hexagon").GetComponent<PlayerHp>();
        playerMove = GameObject.Find("Hexagon").GetComponent<PlayerMove>();

        shotBullet = GameObject.Find("ShotWeapon").GetComponent<Shoot>();
        shotBeam = GameObject.Find("ShotBeam").GetComponent<ShootBeam>();
        shotWave = GameObject.Find("ShotWave").GetComponent<ShootWave>();
        rhythmGameController = GameObject.Find("RhythmBar").GetComponent<RhythmGameController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateStatChanges()
    {
        Hp.SetText("HP: " + playerHp.currentHp + "/" + playerHp.maxHp);
        MS.SetText("MOVE SPEED: " + playerMove.speed);
        Penalty.SetText("PENALTY: " + rhythmGameController.penaltyTime);
        BulletDamage.SetText("BULLET DAMAGE: " + shotBullet.checkCurrentDmg());
        LaserDamage.SetText("LASER DAMAGE: " + shotBeam.beamDmg);
        LaserCount.SetText("LASER COUNT: " + shotBeam.beamNumber);
        WaveDamage.SetText("WAVE DAMAGE: " + shotWave.waveDamage);
        WaveArea.SetText("WAVE AREA: " + shotWave.currentArea());
    }
}
