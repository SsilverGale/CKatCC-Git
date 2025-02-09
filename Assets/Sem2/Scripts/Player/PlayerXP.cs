using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerXP : MonoBehaviour
{
    [SerializeField] int playerXP;
    XP xp;

    void Start()
    {
        xp = GameObject.FindGameObjectWithTag("XPHolder").GetComponent<XP>();
    }

    public void FetchXP()
    {
        playerXP = xp.GetXP();
    }

    public int GetPlayerXP()
    {
    return playerXP;
    }

    public void IncrementXP(int input)
    {
        playerXP += input;
    }
}
