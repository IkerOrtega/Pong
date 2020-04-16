using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Ball ball;
    private Ball ballInstance;
    
    public Paddle paddle;
    private Paddle paddle1, paddle2;
    Text playerPointText, enemyPointText;
    public Text startTimer;
    private Text startTimerInstance;
    public int startTime;
    private float timeLeft;
    public static Vector2 bottomLeft;
    public static Vector2 topRight;
    private int playerPoints = 0, enemyPoints = 0;
    private Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        playerPointText = GameObject.Find("PlayerPoints").GetComponent<Text>();
        enemyPointText = GameObject.Find("EnemyPoints").GetComponent<Text>();

        playerPointText.text = playerPoints.ToString();
        enemyPointText.text = enemyPoints.ToString();

        bottomLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        topRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();


        initialize();


    }

    private void initialize()
    {
        timeLeft = startTime;
        startTimerInstance = Instantiate(startTimer,canvas.transform) as Text;
        paddle1 = Instantiate(paddle);
        paddle2 = Instantiate(paddle);   
        paddle1.init(true);
        paddle2.init(false);
    }

    private void Update()
    {

        if (startTimerInstance != null)
        {
            timeLeft -= Time.deltaTime;
            startTimerInstance.text = timeLeft.ToString("0");
            if (timeLeft < 0)
            {
                Destroy(startTimerInstance.gameObject);
                timeLeft = startTime;
                ballInstance = Instantiate(ball);
                paddle1.updateBallAfterDestroy(ballInstance.gameObject);
                paddle2.updateBallAfterDestroy(ballInstance.gameObject);
            }
        }

        if (ballInstance != null && ballInstance.transform.position.x - ball.transform.localScale.x > topRight.x)
        {
            playerPoints++;
            playerPointText.text = playerPoints.ToString();
            GameObject.Destroy(ballInstance.gameObject);
            startTimerInstance = Instantiate(startTimer, canvas.transform);


        }
        else if (ballInstance != null && ballInstance.transform.position.x + ball.transform.localScale.x < bottomLeft.x)
        {
            enemyPoints++;
            GameObject.Destroy(ballInstance.gameObject);
            enemyPointText.text = enemyPoints.ToString();
            startTimerInstance = Instantiate(startTimer, canvas.transform);

        }
    }
}


