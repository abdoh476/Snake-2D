using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GM : MonoBehaviour {

    public int maxSize;
    public int currentSize;
    public int xBound;
    public int yBound;
    public GameObject[] foodPrefab = new GameObject[4];
    public GameObject[] obstaclesPrefab;
    public GameObject currentObstacle;
    public GameObject currentFood;
    public int score;
    public GameObject snakePrefab;
    public snake head;
    public snake tail;
    public int direction;
    public Vector2 nextPos;
    public Text scoreUI;

   
    // Use this for initialization
    void Start () {
        direction = Random.Range(0, 4);//range from 0 to 3
        InvokeRepeating("timerInvoke", 0, 0.3f);
        foodHandle();
	}
    void timerInvoke()
    {
        movment();
        if (currentSize >= maxSize)
        {
            tailHandle();
        }
        else
        {
            currentSize++;
        }
    }
    void movment()
    {
        GameObject temp;
        nextPos = head.transform.position;
        switch (direction)
        {
            case 0:
                nextPos = new Vector2(nextPos.x, nextPos.y + 0.6f);
                break;
            case 1:
                nextPos = new Vector2(nextPos.x + 0.6f, nextPos.y);
                break;
            case 2:
                nextPos = new Vector2(nextPos.x, nextPos.y - 0.6f);
                break;
            case 3:
                nextPos = new Vector2(nextPos.x - 0.6f, nextPos.y);
                break;
            default:
                break;
        }
        temp = (GameObject)Instantiate(snakePrefab, nextPos, transform.rotation);
        head.setNext(temp.GetComponent<snake>());
        head = temp.GetComponent<snake>();

    }
    // Update is called once per frame
    void Update () {
        changeDirection();
	}
    void changeDirection()
    {
        if (direction!=2 &&Input.GetKeyDown(KeyCode.UpArrow))
        {
            direction = 0;
        }
        if (direction != 3 && Input.GetKeyDown(KeyCode.RightArrow))
        {
            direction = 1;
        }
        if (direction != 0 && Input.GetKeyDown(KeyCode.DownArrow))
        {
            direction = 2;
        }
        if (direction != 1 && Input.GetKeyDown(KeyCode.LeftArrow))
        {
            direction = 3;
        }
    }
    void tailHandle()
    {
        snake temp = tail;
        tail = tail.getNext();
        temp.removeTail();
    }
    void foodHandle()
    {
        int xPos = Random.Range(-xBound, xBound);
        int yPos = Random.Range(-yBound, yBound);
        int index = Random.Range(0, 4);
        currentFood = (GameObject)Instantiate(foodPrefab[index], new Vector2(xPos, yPos), transform.rotation);
        StartCoroutine(checkRender(currentFood));
    }
    IEnumerator checkRender(GameObject IN)
    {
        yield return new WaitForEndOfFrame();
        if (IN.GetComponent<Renderer>().isVisible==false)
        {
            if (IN.tag == "Food")
            {
                Destroy(IN);
                foodHandle();
            }
            if (IN.tag == "obstacle")
            {
                Destroy(IN);
                obstacleHandle();
            }
        }
    }
    void obstacleHandle()
    {
        int xPos = Random.Range(-xBound, xBound);
        int yPos = Random.Range(-yBound, yBound);
        int index = Random.Range(0, 5);
        currentObstacle = (GameObject)Instantiate(obstaclesPrefab[index], new Vector2(xPos, yPos), transform.rotation);
        StartCoroutine(checkRender(currentObstacle));
    }
    void OnEnable()
    {
        snake.hit += hit;
    }
    void OnDisable()
    {
        snake.hit -= hit;
    }
    void hit(string sent)
    {
        if (sent == "Food")
        {
            foodHandle();
            maxSize++;
            score++;
            scoreUI.text = score.ToString();
            if (score > PlayerPrefs.GetInt("HighScore"))
            {
                PlayerPrefs.SetInt("HighScore", score);
            }
            if (score%2==0)
            {
                obstacleHandle();
            }
        }
        if (sent=="Player" || sent== "BackGround" || sent =="obstacle" )
        {
            CancelInvoke("timerInvoke");
            exit();
        }
    }
    public void exit()
    {
        SceneManager.LoadScene(0);
    }
}
