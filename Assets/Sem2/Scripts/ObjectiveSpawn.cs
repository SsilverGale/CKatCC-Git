using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveSpawn : MonoBehaviour
{
    [SerializeField] GameObject DefenceObjectivePrefab;
    [SerializeField] Transform[] DefenceObjectiveSpawnpoints;
    WaveSpawn WS;
    FloorIsHotsauce FIHS;

    // Start is called before the first frame update
    void Start()
    {
        FIHS = GameObject.FindWithTag("FIHS").GetComponent<FloorIsHotsauce>();
        WS = GameObject.FindWithTag("WaveSpawn").GetComponent<WaveSpawn>();
    }

    public void NewObjective()
    {
        int random = Random.Range(0, 2);
        //Floor is Hotsauce
        if (random == 0)
        {
            Debug.Log("Hotsauce is Rising!");
            FIHS.StartRise();
        }
        //Mini boss
        if (random == 2)
        {
            Debug.Log("Enemy Empowered!");
            WS.MinibossEmpower();
        }
        //Defend
        if (random == 1) 
        {
            Debug.Log("Defend!");
            int random2 = Random.Range(0,DefenceObjectiveSpawnpoints.Length);
            Instantiate(DefenceObjectivePrefab, DefenceObjectiveSpawnpoints[random2].position, Quaternion.identity);
            GameObject[] temp = GameObject.FindGameObjectsWithTag("BurgerEnemy");
            foreach (GameObject gameObject in temp) 
            {
                gameObject.GetComponent<BurgerNavi>().DefenceObjectiveSet(true);
            }
            temp = GameObject.FindGameObjectsWithTag("HotdogEnemy");
            foreach (GameObject gameObject in temp)
            {
                gameObject.GetComponent<HotdogNavi>().DefenceObjectiveSet(true);
            }
            temp = GameObject.FindGameObjectsWithTag("PopcornEnemy");
            foreach (GameObject gameObject in temp)
            {
                gameObject.GetComponent<PopcornNav>().DefenceObjectiveSet(true);
            }
        }
    }
}
