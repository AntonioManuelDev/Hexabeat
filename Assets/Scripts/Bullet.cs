using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float maxDistance = 10f; // Distancia máxima que recorrerá la bala antes de destruirse
    public int bulletDmg = 10;
    private float distanceTraveled = 0f;
    private Vector2 initialPosition;
    public List<EnemyMove> enemies = new List<EnemyMove>();

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        // Mover la bala hacia adelante
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        // Calcular la distancia recorrida desde la posición inicial
        distanceTraveled = Vector2.Distance(transform.position, initialPosition);

        // Destruir la bala si recorre la distancia máxima
        if (distanceTraveled >= maxDistance)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyMove>().getDamagedEnemy(this,bulletDmg);
            Destroy(gameObject);
        }
    }
}