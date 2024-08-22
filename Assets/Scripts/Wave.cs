using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    ShootWave shotWave;

    private void Start()
    {
        shotWave= GameObject.Find("ShotWave").GetComponent<ShootWave>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyMove>().getDamagedEnemyW(this, shotWave.waveDamage);
        }
    }
}
