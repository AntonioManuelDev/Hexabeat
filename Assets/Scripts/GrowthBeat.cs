using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowthBeat : MonoBehaviour
{
    public float beatTime;
    public float maxGrowthFactor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.localScale == Vector3.one)
        {
            StartCoroutine(c_Beat());
        }
    }

    IEnumerator c_Beat()
    {
        float growthFactor = (maxGrowthFactor-1)/beatTime;
        while (transform.localScale.x <= maxGrowthFactor) 
        {
            transform.localScale = new Vector3(transform.localScale.x+growthFactor,transform.localScale.y+growthFactor,transform.localScale.z);
        }
        yield return new WaitForSeconds(beatTime);
        transform.localScale = Vector3.one;
    }
}
