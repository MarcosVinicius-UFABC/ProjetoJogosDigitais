using UnityEngine;
using TMPro;

public class GameManagerTimer : MonoBehaviour
{
    public TextMeshPro textMeshPro;
    private TextMeshPro clock;

    public float sceneTime = 300f;
    private float timer;

    private GameObject player;

    public Vector3 clockOffset;

    public GameObject victoryScreen;
    private int minutesNumber;
    private string minutes;
    private int secondsNumber;
    private string seconds;

    private bool gameOver = false;

    void Start()
    {
        timer = sceneTime;
        clock = Instantiate(textMeshPro);
        clock.transform.position = clockOffset;
        clock.transform.SetParent(this.transform);
        minutesNumber = (int)(timer/60);
        minutes = minutesNumber.ToString();
        secondsNumber = (int)(timer%60);
        seconds = secondsNumber<10? "0" : "" + secondsNumber.ToString();
        clock.text = minutes + ":" + seconds;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if(player != null)
        {
            timer -= Time.deltaTime;
            minutesNumber = (int)(timer/60);
            minutes = minutesNumber.ToString();
            secondsNumber = (int)(timer%60);
            seconds = (secondsNumber<10? "0" : "") + secondsNumber.ToString();
            clock.text = minutes + ":" + seconds;
        }

        if(timer <= 0 && !gameOver)
        {
            GameObject [] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].GetComponent<EnemyHealth>().IsDead();
            }

            Destroy(clock);

            gameObject.GetComponent<VictoryScreen>().Pause();
            gameOver = true;
        }
    }
}
