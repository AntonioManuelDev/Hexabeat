using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 tempVect = new Vector3(h, v, 0);
        transform.position += tempVect.normalized * speed * Time.deltaTime;
        donotMoveAtLimits();
    }

    void tpAtLimits()
    {
        if (transform.position.y > 25)
        {
            transform.position = new Vector3(-transform.position.x, -25, 0);
        }
        if (transform.position.y < -25)
        {
            transform.position = new Vector3(-transform.position.x, 25, 0);
        }
        if (transform.position.x > 25)
        {
            transform.position = new Vector3(-25, -transform.position.y, 0);
        }
        if (transform.position.x < -25)
        {
            transform.position = new Vector3(25, -transform.position.y, 0);
        }
    }
    void donotMoveAtLimits()
    {
        if (transform.position.y > 25)
        {
            transform.position = new Vector3(transform.position.x, 25, 0);
        }
        if (transform.position.y < -25)
        {
            transform.position = new Vector3(transform.position.x, -25, 0);
        }
        if (transform.position.x > 25)
        {
            transform.position = new Vector3(25, transform.position.y, 0);
        }
        if (transform.position.x < -25)
        {
            transform.position = new Vector3(-25, transform.position.y, 0);
        }
    }
}