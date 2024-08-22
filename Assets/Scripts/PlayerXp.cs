using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerXp : MonoBehaviour
{
    public Image xpBar;
    public TextMeshProUGUI lvl;
    public TextMeshProUGUI score;
    public int maxXp = 100;
    public int currentXp = 0;
    public GameObject levelUp;
    public int currentLvl = 0;
    public bool activated = false;
    public LevelUpUpgradesSelect levelUpgradesSelect;
    public int scoreNumber = 0;
    //int damage = 15;
    // Start is called before the first frame update
    void Start()
    {
        xpBar = GameObject.Find("XpBar").GetComponent<Image>();
        levelUp = GameObject.Find("LevelUp");
        levelUp.SetActive(false);
        lvl = GameObject.Find("Lvl").GetComponent<TextMeshProUGUI>();
        score = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
        StartCoroutine(c_addScore());
    }

    public void getXp(int xp)
    {   
        currentXp += xp;
        scoreNumber += xp;
        score.SetText("Score: " + scoreNumber.ToString());
        if (currentXp >= maxXp)
        {
            currentXp -= maxXp;
            currentLvl++;
            levelUp.SetActive(true);
            activated= true;
            Time.timeScale = 0;
            maxXp += 5;
            lvl.SetText(currentLvl.ToString());
            levelUpgradesSelect.rollUpgrades();
        }
        xpBar.fillAmount = currentXp / (float)maxXp;
    }

    public void closeUpgradeTab()
    {
        levelUp.SetActive(false);
        activated = false;
        Time.timeScale = 1;
    }

    IEnumerator c_addScore()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            scoreNumber += 10;
            score.SetText("Score: " + scoreNumber.ToString());
        }
    }
}