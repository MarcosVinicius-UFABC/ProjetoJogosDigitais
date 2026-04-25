using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] public GameObject enemy;

    public int baseEnemyCount = 5;
    public int enemiesPerWave = 3;
    public float timeBetweenWaves = 5f;
    public float spawnInterval = 0.3f;

    public int KillCount { get; private set; } = 0;

    private GameObject player;
    private float height;
    private float width;
    private int currentWave = 0;
    private int enemiesAlive = 0;
    private bool nextWavePending = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        Camera cam = Camera.main;
        height = 2f * cam.orthographicSize;
        width = height * cam.aspect;

        StartCoroutine(NextWave());
    }

    IEnumerator NextWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        currentWave++;
        int waveSize = baseEnemyCount + (currentWave - 1) * enemiesPerWave;
        Debug.Log($"Wave {currentWave} — {waveSize} enemies");
        yield return StartCoroutine(SpawnWave(waveSize));
    }

    IEnumerator SpawnWave(int count)
    {
        nextWavePending = false;
        enemiesAlive = count;
        for (int i = 0; i < count; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnEnemy()
    {
        Vector3 pos = GetEdgeSpawnPosition();
        GameObject newEnemy = Instantiate(enemy, pos, Quaternion.identity);
        newEnemy.GetComponent<EnemyController>().SetTarget(player);
    }

    public void EnemyDied()
    {
        enemiesAlive--;
        KillCount++;
        if (enemiesAlive <= 0 && !nextWavePending)
        {
            nextWavePending = true;
            StartCoroutine(NextWave());
        }
    }

    Vector3 GetEdgeSpawnPosition()
    {
        Vector3 center;
        if(player != null)
        {
            center = player.transform.position;
        }
        else
        {
            center = this.transform.position;
        }
        float margin = 1.5f;
        int edge = Random.Range(0, 4);
        float x, y;

        switch (edge)
        {
            case 0: x = Random.Range(center.x - width / 2, center.x + width / 2); y = center.y + height / 2 + margin; break;
            case 1: x = Random.Range(center.x - width / 2, center.x + width / 2); y = center.y - height / 2 - margin; break;
            case 2: x = center.x - width / 2 - margin; y = Random.Range(center.y - height / 2, center.y + height / 2); break;
            default: x = center.x + width / 2 + margin; y = Random.Range(center.y - height / 2, center.y + height / 2); break;
        }

        return new Vector3(x, y, 0f);
    }
}
