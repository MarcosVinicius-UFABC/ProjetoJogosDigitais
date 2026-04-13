using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [SerializeField] public GameObject enemy;

    private GameObject player;
    private Camera cam;
    private float height;
    private float width;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        Camera cam = Camera.main;
        height = 2f * cam.orthographicSize;
        width = height * cam.aspect;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            StartCoroutine(WaveSpawn(10));
            print("ESPAÇO");
        }
    }
    IEnumerator WaveSpawn(int waveSize)
    {
        for (int i = 0; i < waveSize; i++)
        {
            EnemySpawn();
            yield return new WaitForEndOfFrame();
        }
    }

    void EnemySpawn()
    {
        Vector3 spawnPosition = this.transform.position + new Vector3(Random.Range(-width,width), Random.Range(-height,height) * Random.Range(1.2f,2f), 0);
        GameObject newEnemy = Instantiate(enemy, spawnPosition, Quaternion.identity);
        newEnemy.GetComponent<EnemyController>().SetTarget(player);
    }
}
