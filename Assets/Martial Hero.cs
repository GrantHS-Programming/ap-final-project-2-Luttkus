using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MartialHero : MonoBehaviour
{
    private float thrust = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        //idle_0 = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log("this is working");
        if (Input.GetKeyDown("up"))
        {
            //GetComponent<Rigidbody2D>().AddForce(transform.up * thrust, ForceMode2D.Impulse);
            //idle_0.velocity = new Vector2(0.0f, thrust);
            
        }

    }
}
