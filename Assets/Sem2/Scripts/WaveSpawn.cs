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
    [SerializeField] bool isWaveBeat;
    [SerializeField] int enemyCount = 1;
    [SerializeField] int waveCount = 0;
    [SerializeField] bool enableSpawn = false;
    bool isClick = false;
    [SerializeField] int spawnPointcount = 0;
    bool waitForDelay = false;
    [SerializeField] int maxEnemyCount = 20;
    int SpawnCount = 0;
    [SerializeField] GameObject WinUI;
    bool empowerEnemy = false;
    bool enableEmpower = false;
    [SerializeField] int BossEnemyTag;
    [SerializeField] int ObjectiveRandomizer = 0;
    ObjectiveSpawn OS;
    UI ui;
    XP xp;
    bool ready = false;
    bool counterCooldown = false;
    [SerializeField] int pauseCounter = 30;

    void Start()
    {
        xp = GameObject.FindGameObjectWithTag("XPHolder").GetComponent<XP>();
        ui = GameObject.FindGameObjectWithTag("UI").GetComponent<UI>();
        OS = GetComponent<ObjectiveSpawn>();
        WinUI.SetActive(false);
        isWaveBeat = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            ready = !ready;
            if (ready) 
            {
                pauseCounter -= 20;
            }
            if (!ready)
            {
                pauseCounter += 20;
            }
        }
        if (isClick) 
        {
            if (waveCount >= 1 && enemyCount <= 0 && counterCooldown == false)
            {
                ui.PauseWave(true);
                StartCoroutine(Wait());
            }
            if (pauseCounter <= 0) 
            {
                isWaveBeat = false;
                pauseCounter = 30;
            }
            //for review 1
            if (waveCount == 5)
            {
                WinUI.SetActive(true);
                enemyCount = 1;
            }
            if (enemyCount <= 0 && !isWaveBeat)
            {
                    ui.PauseWave(false);
                    waveCount++;
                    ui.UpdateWaveCount(waveCount);
                    ObjectiveRandomizer = Random.Range(0, (int)(enemyCount - Mathf.Round((float)(enemyCount * 0.6f))));
                    maxEnemyCount = (int)(maxEnemyCount * 1.2f);
                    SpawnCount = 0;
                    isWaveBeat = true;
                    enableSpawn = true;
                    xp.AddXP(300);                      
            }
            if (SpawnCount == ObjectiveRandomizer)
            {
                OS.NewObjective();
            }
            if (enableEmpower) 
            {
                if (SpawnCount == BossEnemyTag)
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
                                    empowerEnemy = false;
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
                                empowerEnemy = false;
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
                                    empowerEnemy = false;   
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


    public void enableIsClick()
    {
        isClick = true;
    }

    public void MinibossEmpower()
    {
        BossEnemyTag = Random.Range(0,maxEnemyCount - 4);
        Debug.Log(BossEnemyTag);
        enableEmpower = true;
    }
    IEnumerator Wait()
    {
        counterCooldown = true;
        yield return new WaitForSeconds(1);
        pauseCounter--;
        counterCooldown = false;
    }

    public int GetPauseTime()
    {
        return pauseCounter;
    }
}
