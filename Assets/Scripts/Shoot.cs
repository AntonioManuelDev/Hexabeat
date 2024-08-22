using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float shootDistance = 10f;

    public void shootBullet()
    {
        // Encontrar el enemigo más cercano
        GameObject nearestEnemy = FindNearestEnemy();

        if (nearestEnemy != null)
        {
            // Instanciar una bala
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

            // Apuntar la bala hacia el enemigo
            Vector2 direction = (nearestEnemy.transform.position - transform.position).normalized;
            bullet.transform.right = direction;
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

    public void increaseBulletDmg()
    {
        Bullet bullets = bulletPrefab.GetComponent<Bullet>();
        bullets.bulletDmg += 3;
    }

    public int checkCurrentDmg()
    {
        Bullet bullets = bulletPrefab.GetComponent<Bullet>();
        return bullets.bulletDmg;
    }
}