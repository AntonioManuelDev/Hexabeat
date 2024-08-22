using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserImpact : MonoBehaviour
{
    public int laserDmg = 1;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyMove>().getDamagedEnemyL(this, laserDmg);
        }
    }
}