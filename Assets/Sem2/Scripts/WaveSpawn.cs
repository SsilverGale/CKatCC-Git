using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawn : MonoBehaviour
{
    [SerializeField] Transform[] defaultSpawnPoints = new Transform[4];
    [SerializeField] Transform[] advancedSpawnPoints = new Transform[6];
    bool enableAdvancedSpawn = false;
    [SerializeField] GameObject BurgerEnemy; 
    [SerializeField] GameObject HotdogEnemy; 
    [SerializeField] GameObject PopcornEnemy;
    [SerializeField] GameObject BurgerBossEnemy;
    [SerializeField] GameObject HotdogBossEnemy;
    [SerializeField] GameObject PopcornBossEnemy;
    bool isWaveBeat = true;
    [SerializeField] int enemyCount = 0;
    [SerializeField] int waveCount = 0;
    [SerializeField] bool enableSpawn = false;
    bool isClick = false;
    int spawnPointcount = 0;
    bool waitForDelay = false;
    int maxEnemyCount = 20;
    int SpawnCount = 0;
    [SerializeField] GameObject WinUI;
    bool empowerEnemy = false;
    bool enableEmpower = false;
    int totalSpawnCount = 0;
    int BossEnemyTag;
    int ObjectiveRandomizer = 0;
    ObjectiveSpawn OS;

    void Start()
    {
        OS = GetComponent<ObjectiveSpawn>();
        WinUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Invoke("enableIsClick", 1);
        }
        if (isClick) 
        {
            //for review 1
            if (waveCount == 5)
            {
                WinUI.SetActive(true);
                enemyCount = 1;
            }
            if (enemyCount == 0)
            {
                waveCount++;
                ObjectiveRandomizer = Random.Range(0, (int)(enemyCount - Mathf.Round((float)(enemyCount * 0.6f))));
                maxEnemyCount = (int)(maxEnemyCount * 1.2f);
                SpawnCount = 0;
                isWaveBeat = true;
                enableSpawn = true;              
                totalSpawnCount = 0;
            }
            if (totalSpawnCount == ObjectiveRandomizer)
            {
                OS.NewObjective();
            }
            if (enableEmpower) 
            {
                if (totalSpawnCount == BossEnemyTag)
                {
                    empowerEnemy = true;
                    enableEmpower = false;
                    BossEnemyTag = 0;
                }
            }
            if (isWaveBeat && SpawnCount < maxEnemyCount)
            {             
                    float random = Random.Range(0, 100);
                    if (enableSpawn == true)
                    {
                        
                            if (random >= 0 && random < 50)
                            {
                                if (empowerEnemy)
                                {
                                    Instantiate(BurgerBossEnemy, defaultSpawnPoints[spawnPointcount].transform.position, Quaternion.identity);
                                }
                                else
                                {
                                    Instantiate(BurgerEnemy, defaultSpawnPoints[spawnPointcount].transform.position, Quaternion.identity);
                                }                          
                            }
                            else if (random >= 50 && random < 85)
                            {
                                if (empowerEnemy)
                                {
                                    Instantiate(HotdogBossEnemy, defaultSpawnPoints[spawnPointcount].transform.position, Quaternion.identity);
                                }
                                else
                                {
                                    Instantiate(HotdogEnemy, defaultSpawnPoints[spawnPointcount].transform.position, Quaternion.identity);
                                }
                            }
                            else if (random >= 85 && random < 101)
                            {
                                if (empowerEnemy)
                                {
                                    Instantiate(PopcornBossEnemy, defaultSpawnPoints[spawnPointcount].transform.position, Quaternion.identity);
                                }
                                else
                                {
                                    Instantiate(PopcornEnemy, defaultSpawnPoints[spawnPointcount].transform.position, Quaternion.identity);
                                }
                            }
                            spawnPointcount++;
                            if (!enableAdvancedSpawn && spawnPointcount == 4)
                            {
                                spawnPointcount = 0;
                            }
                            SpawnCount++;
                            enemyCount++;
                            totalSpawnCount++;
                            StartCoroutine(enemySpawnCooldown());
                    }              
            }
        }     
    }

    public void DecreaseEnemyCount()
    {
        enemyCount--;
    }

    IEnumerator enemySpawnCooldown()
    {
        enableSpawn = false;
        yield return new WaitForSeconds(1);
        enableSpawn = true;
    }


    void enableIsClick()
    {
        isClick = true;
    }

    public void MinibossEmpower()
    {
        BossEnemyTag = Random.Range(0,maxEnemyCount - 4);
        Debug.Log(BossEnemyTag);
        enableEmpower = true;
    }
}
