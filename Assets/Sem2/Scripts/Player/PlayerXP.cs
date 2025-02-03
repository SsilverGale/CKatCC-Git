using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerXP : MonoBehaviour
{
    int playerXP;
    XP xp;

     void Start()
    {
        xp = GameObject.FindGameObjectWithTag("XPHolder").GetComponent<XP>();
    }

    public void FetchXP()
    {
        xp.GetXP();
    }
}
