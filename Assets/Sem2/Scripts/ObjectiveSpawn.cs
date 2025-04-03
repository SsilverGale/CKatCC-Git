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
            random = -1;
        }
        //Mini boss
        if (random == 1)
        {
            Debug.Log("Enemy Empowered!");
            WS.MinibossEmpower();
            random = -1;
        }
    }

}
