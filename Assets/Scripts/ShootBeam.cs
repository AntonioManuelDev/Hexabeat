using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBeam : MonoBehaviour
{
    public GameObject beamPrefab;
    public int beamNumber = 1;
    public int beamDmg = 1;
    //public GameObject[] beamArray;
    public void shootBeam()
    {
        // Encontrar el enemigo más cercano
        GameObject nearestEnemy = FindNearestEnemy();

        if (nearestEnemy != null)
        {
            Vector2 direction = (nearestEnemy.transform.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            for (int i = 1; i <= beamNumber; i++)
            {
                GameObject laser = Instantiate(beamPrefab, transform.position, Quaternion.identity,transform);
                float rotAngle = gameObject.transform.GetChild(i-1).GetComponent<Laser>().rotationAngle;
                laser.transform.rotation = Quaternion.Euler(Vector3.forward * (angle - (90f - rotAngle / 2) + (float)(360 / beamNumber) * i));
                laser.transform.parent = transform;
            }
        }
    }
    GameObject FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearestEnemy = null;
        float nearestDistance = Mathf.Infinity;
        Vector2 playerPosition = transform.position;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(playerPosition, enemy.transform.position);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestEnemy = enemy;
            }
        }
        return nearestEnemy;
    }
}
