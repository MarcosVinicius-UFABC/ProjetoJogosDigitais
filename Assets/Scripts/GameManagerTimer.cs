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

    private bool gameOver = false;

    void Start()
    {
        timer = sceneTime;
        clock = Instantiate(textMeshPro);
        clock.transform.position = clockOffset;
        clock.transform.SetParent(this.transform);
        clock.text = Mathf.RoundToInt(timer/60) + ":" + Mathf.RoundToInt(timer%60);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if(player != null)
        {
            timer -= Time.deltaTime;
            clock.text = (int)(timer/60) + ":" + (int)(timer%60);
        }

        if(timer <= 0 && !gameOver)
        {
            GameObject [] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].GetComponent<EnemyHealth>().IsDead();
            }

            gameObject.GetComponent<VictoryScreen>().Pause();
            gameOver = true;
        }
    }
}
