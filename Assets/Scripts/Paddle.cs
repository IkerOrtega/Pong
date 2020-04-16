using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rigidbody;
    private bool isPlayer = false;
    private GameObject ball;
    // Start is called before the first frame update
    void Start()
    {
        ball = GameObject.FindGameObjectWithTag("Ball");
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public void updateBallAfterDestroy(GameObject ballInstance)
    {
        ball = ballInstance;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isPlayer)
        {
            
            if (Input.GetAxis("Vertical") > 0 && transform.position.y <= GameManager.topRight.y - transform.localScale.y / 2)
            {
                rigidbody.velocity = new Vector2(0,speed);
            }
            else if (Input.GetAxis("Vertical") < 0 && transform.position.y >= GameManager.bottomLeft.y + transform.localScale.y / 2)
            {
                rigidbody.velocity = new Vector2(0, -speed);
            }
            else
            {
                rigidbody.velocity = Vector2.zero;
            }
        }
        else
        {
            moveEnemyPaddle();
        }
       
    }
    private void moveEnemyPaddle()
    {
        print("position: " + transform.position.y + "\n Scale: " + transform.localScale.y / 2);
        if (ball != null && ball.transform.position.y > transform.position.y + transform.localScale.y / 2 && transform.position.y + transform.localScale.y / 2 < GameManager.topRight.y)
        {
            rigidbody.velocity = new Vector2(0, speed);
        }
        else if (ball != null && ball.transform.position.y < transform.position.y - transform.localScale.y / 2 && transform.position.y - transform.localScale.y / 2 > GameManager.bottomLeft.y)
        {
            rigidbody.velocity = new Vector2(0, -speed);
        } else
        {
            rigidbody.velocity = Vector2.zero;
        }

    }

    public void init(bool isRight)
    {
        speed = 5f;
        isPlayer = !isRight;
        Vector2 pos = Vector2.zero;
        if(isRight)
        {
            pos = new Vector2(GameManager.topRight.x,0);
            pos -= Vector2.right * transform.localScale.x;

        } else
        {
            pos = new Vector2(GameManager.bottomLeft.x,0);
            pos += Vector2.right * transform.localScale.x;
        }
        transform.position = pos;
    }
}
