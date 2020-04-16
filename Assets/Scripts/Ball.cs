using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{

    public float speed;
    public Vector2 direction;
    private Rigidbody2D rigidbody;
 
    // Start is called before the first frame update
    void Start()
    {
        speed = 10f;
        rigidbody = GetComponent<Rigidbody2D>();
        float initAngle = Random.Range(-0.3f, 0.3f);
        direction = new Vector2(1.0f, initAngle);
        if (Random.Range(-1, 1) < 0)
        {
            direction.x *= -1;
        }

        rigidbody.velocity = direction * speed;
    }

   

    // Update is called once per frame
    void Update()
    {


        if (transform.position.y >= GameManager.topRight.y - transform.localScale.y / 2 && direction.y > 0)
        {
            direction.y *= -1;
            direction.y += Random.Range(0.0f, -0.1f);
            rigidbody.velocity = direction * speed;
            
        }
        else if (transform.position.y <= GameManager.bottomLeft.y + transform.localScale.y / 2 && direction.y < 0)
        {
            direction.y *= -1;
            direction.y += Random.Range(0.0f, 0.1f);
            rigidbody.velocity = direction * speed;
        }
/*
        if (transform.position.y >= GameManager.topRight.y - transform.localScale.y/2
            || transform.position.y <= GameManager.bottomLeft.y + transform.localScale.y/2)
        {
            direction.y *= -1;
            rigidbody.velocity = direction * speed;
            print("Direction: " + direction);
        }
        */
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.transform.position.y + collision.gameObject.transform.localScale.y/6 < transform.position.y )
        {
            direction.y += Random.Range(0.0f, 0.3f);
        }
        else if(collision.gameObject.transform.position.y - collision.gameObject.transform.localScale.y / 6 > transform.position.y)
        {
            direction.y -= Random.Range(0.0f, 0.3f); 
        }
        direction.x *= -1;
        rigidbody.velocity = direction * speed;

    }



}
