using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject trianglePrefab;
    public GameObject squarePrefab;
    public GameObject pentagonPrefab;
    public float timeCountMin = 0.1f;
    public float timeCountMax = 5.0f;
    float currentCount;
    bool spawnNow = true;
    Vector3 randPos;
    // Start is called before the first frame update
    void Start()
    {
        currentCount = timeCountMax;
    }

    // Update is called once per frame
    void Update()
    {
        if(spawnNow)
        {
            int r = Random.Range(0, 3);
            if (r == 0)
            {
                StartCoroutine(c_SpawnRandomlyTriangle());
            }
            if(r == 1)
            {
                StartCoroutine(c_SpawnRandomlySquare());
            }
            if(r == 2)
            {
                StartCoroutine(c_SpawnRandomlyPentagon());
            }
        }
    }

    IEnumerator c_SpawnRandomlyTriangle()
    {
        randomizeRandPos();
        spawnNow = false;
        Instantiate(trianglePrefab,randPos,Quaternion.identity);
        yield return new WaitForSeconds(currentCount);
        spawnNow = true;
        if (currentCount > timeCountMin)
        {
            currentCount -= 0.05f;
        }
    }
    
    IEnumerator c_SpawnRandomlySquare()
    {
        randomizeRandPos();
        spawnNow = false;
        Instantiate(squarePrefab,randPos,Quaternion.identity);
        yield return new WaitForSeconds(currentCount+0.5f);
        spawnNow = true;
        if (currentCount > timeCountMin)
        {
            currentCount -= 0.05f;
        }
    }
    IEnumerator c_SpawnRandomlyPentagon()
    {
        randomizeRandPos();
        spawnNow = false;
        Instantiate(pentagonPrefab,randPos,Quaternion.identity);
        yield return new WaitForSeconds(currentCount+0.9f);
        spawnNow = true;
        if (currentCount > timeCountMin)
        {
            currentCount -= 0.05f;
        }
    }

    void randomizeRandPos()
    {
        float randY = Random.Range(-50,50);
        float randX = Random.Range(-50,50);
        float determiner = Random.Range(-100, 100);
        float side = Random.Range(-100, 100);
        
        if(determiner < 0 ) 
        { 
            if(side > 0)
            {
                randPos = new Vector3(randX, 50, 0);
            } else
            {
                randPos = new Vector3(randX, -50, 0);
            }
        } else
        {
            if(side > 0) 
            {
                randPos = new Vector3(50, randY, 0);
            } else
            {
                randPos = new Vector3(-50,randY, 0);
            }
        }
    }
}