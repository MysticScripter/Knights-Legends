using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour
{
    public float spawnTime = 5f;
    public float waveTime = 60f;
    public float bossHealth = 5f;
    public float zombieHealth = 1f;
    public GameObject[] enemies;
    private float spawnTimeCurrent;
    private float wavetimer;
    private float spawntimer;
    public Transform[] spawnPoints;
    public PlayerHealth playerHealth;

    IEnumerator GoToMainMenu()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(0);
    }

    private void Start()
    {
        spawnTimeCurrent = spawnTime;
    }

    private void Update()
    {
        wavetimer += Time.deltaTime;
        spawntimer += Time.deltaTime;
        if (playerHealth.health <= 0.0)
        {
            StartCoroutine("GoToMainMenu");
        }
        else
        {
            if (wavetimer > waveTime)
            {
                wavetimer = 0.0f;
                ++waveTime;
                spawnTime = 1000f;
                KillAll();
                BossSpawn();
                bossHealth += 5f;
                ++zombieHealth;
            }
            if (spawntimer > spawnTime)
            {
                spawntimer = 0.0f;
                Spawn();
            }
            if (spawnTime == 1000.0 && GameObject.FindGameObjectWithTag("Zombie") == null)
            {
                wavetimer = 0.0f;
                spawnTime = spawnTimeCurrent;
            }
              
            
        }
    }

    private void KillAll()
    {
        GameObject[] zombies = GameObject.FindGameObjectsWithTag("Zombie");
        foreach (GameObject zombie in zombies)
            zombie.GetComponent<EnemyHealth>().health = 0f;
    }

    private void BossSpawn()
    {
        int index1 = Random.Range(0, spawnPoints.Length);
        int index2 = Random.Range(0, enemies.Length);
        GameObject zombie = Instantiate(enemies[index2], spawnPoints[index1].position, spawnPoints[index1].rotation);
        zombie.name = enemies[index2].name;
        zombie.transform.localScale = new Vector3(0.6f,0.6f,0.6f);
        zombie.GetComponent<EnemyHealth>().startHealth = bossHealth;
        zombie.GetComponent<EnemyHealth>().health = bossHealth;
    }

    private void Spawn()
    {
        int index1 = Random.Range(0, spawnPoints.Length);
        int index2 = Random.Range(0, enemies.Length);
        GameObject zombie = Instantiate(enemies[index2], spawnPoints[index1].position, spawnPoints[index1].rotation);
        zombie.name = enemies[index2].name;
        zombie.GetComponent<EnemyHealth>().startHealth = zombieHealth;
        zombie.GetComponent<EnemyHealth>().health = zombieHealth;
    }
}
