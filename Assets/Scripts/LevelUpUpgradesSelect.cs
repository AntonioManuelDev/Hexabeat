using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpUpgradesSelect : MonoBehaviour
{
    public GameObject bulletLvlUp, beamLvlUp, waveLvlUp, hpLvlUp, msLvlUp, penaltyLvlUp;
    public List<GameObject> allUpgrades;

    void Start()
    {
        // Inicializa la lista con todos los GameObjects
        allUpgrades = new List<GameObject> { bulletLvlUp, beamLvlUp, waveLvlUp, hpLvlUp, msLvlUp, penaltyLvlUp };
    }

    public void rollUpgrades()
    {
        // Desactivar todos los GameObjects al principio
        foreach (GameObject upgrade in allUpgrades)
        {
            upgrade.SetActive(false);
        }

        // Crear una lista temporal para seleccionar aleatoriamente
        List<GameObject> tempUpgrades = new List<GameObject>(allUpgrades);

        // Activar 3 GameObjects aleatoriamente
        for (int i = 0; i < 3; i++)
        {
            int randomIndex = Random.Range(0, tempUpgrades.Count);
            tempUpgrades[randomIndex].SetActive(true);
            tempUpgrades.RemoveAt(randomIndex); // Eliminar para no seleccionarlo de nuevo
        }
    }

    public void maxPenaltyUpgraded()
    {
        penaltyLvlUp.SetActive(false);
        allUpgrades.Remove(penaltyLvlUp);
    }
}
