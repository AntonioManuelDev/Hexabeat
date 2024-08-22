using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHp : MonoBehaviour
{
    public Image hpBar;
    public int maxHp = 100;
    public int HpRegen = 0;
    public int currentHp;
    bool recentDmg = false;
    public float inmunityTime = 0.5f;
    public float inmunityDeltaTime = 0.15f;
    public GameObject deathScreen;
    public PlayerXp playerXp;
    public TextMeshProUGUI finalScore;

    // Start is called before the first frame update
    void Start()
    {
        currentHp = maxHp;
        hpBar = GameObject.Find("HpBar").GetComponent<Image>();
        deathScreen = GameObject.Find("DeathScreen");
        finalScore = GameObject.Find("FinalScore").GetComponent<TextMeshProUGUI>();
        deathScreen.SetActive(false);
        playerXp = GameObject.Find("Hexagon").GetComponent<PlayerXp>();
        StartCoroutine(hpRegenPerSec()); // Iniciar la regeneración de HP al comenzar el juego
    }

    public void getDamaged(int damage)
    {
        if (!recentDmg)
        {
            currentHp -= damage;
            hpBar.fillAmount = currentHp / (float)maxHp;
            if(currentHp <= 0)
            {
                deathScreen.SetActive(true);
                finalScore.SetText("Score: " + playerXp.scoreNumber.ToString());
                gameObject.GetComponent<Collider2D>().enabled = false;
            }
            if (currentHp <= maxHp * 0.5 && currentHp > maxHp * 0.15)
            {
                hpBar.color = Color.yellow;
            }
            if (currentHp <= maxHp * 0.15)
            {
                hpBar.color = Color.red;
            }
            recentDmg = true;
            StartCoroutine(dmgDelay());
        }
    }

    IEnumerator dmgDelay()
    {
        for (float i = 0; i < inmunityTime; i += inmunityDeltaTime)
        {
            if (transform.localScale == Vector3.one)
            {
                transform.localScale = Vector3.zero;
            }
            else
                transform.localScale = Vector3.one;

            yield return new WaitForSeconds(inmunityDeltaTime);
        }
        transform.localScale = Vector3.one;
        recentDmg = false;
    }

    IEnumerator hpRegenPerSec()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f); // Esperar 1 segundo
            if (currentHp < maxHp)
            {
                currentHp += HpRegen;
                if (currentHp > maxHp)
                {
                    currentHp = maxHp;
                }
                hpBar.fillAmount = currentHp / (float)maxHp;
                if (currentHp <= maxHp * 0.5 && currentHp > maxHp * 0.15)
                {
                    hpBar.color = Color.yellow;
                }
                if (currentHp > maxHp * 0.5)
                {
                    hpBar.color = Color.green;
                }
            }
        }
    }
}
