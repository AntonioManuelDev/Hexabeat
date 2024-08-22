using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public PlayerHp playerHp;
    Transform player;
    PlayerXp playerXp;
    public float speed = 2f;
    public int contactDamage = 5;
    bool dealingDmg = false;
    public int hp = 12;
    public int xpGiven = 20;
    public List<Bullet> impacted = new List<Bullet>();
    private BeatManager bManager;
    public bool alive = true;
    // Start is called before the first frame update
    void Start()
    {   
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();      
        playerHp = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHp>();      
        playerXp = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerXp>();
        bManager = GameObject.Find("AudioManager").GetComponent<BeatManager>();      
        bManager.AddMe(gameObject);

        speed = 2f + (playerXp.currentLvl * 0.2f);
        contactDamage = 5 + playerXp.currentLvl * 2;
        hp = 12 + playerXp.currentLvl * 3;
        xpGiven = 20 + playerXp.currentLvl * 2;
    }

    // Update is called once per frame
    void Update()
    {
        if(dealingDmg)
        {
            playerHp.getDamaged(contactDamage);
        }
        var offset = 30f;
        Vector2 direction = player.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }

    public void getDamagedEnemy(Bullet bullet, int dmg)
    {
        if (!bullet.enemies.Contains(this) && !impacted.Contains(bullet))
        {
            bullet.enemies.Add(this);
            impacted.Add(bullet);
            hp -= dmg;
        }
        if(hp <= 0)
        {
            Destroy(gameObject);
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerXp>().getXp(xpGiven);
        }
    }
    public void getDamagedEnemyL(LaserImpact laser, int dmg)
    {
        hp -= dmg;
        if(hp <= 0)
        {
            Destroy(gameObject);
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerXp>().getXp(xpGiven);
        }
    }
    public void getDamagedEnemyW(Wave wave, int dmg)
    {
        hp -= dmg;
        if(hp <= 0)
        {
            Destroy(gameObject);
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerXp>().getXp(xpGiven);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("contact");
        if(collision.tag == "Player")
        {
            dealingDmg = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            dealingDmg = false;
        }
    }
}