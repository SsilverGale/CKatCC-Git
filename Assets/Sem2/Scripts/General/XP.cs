using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XP : MonoBehaviour
{
    [SerializeField] int TotalXP;
    public void AddXP(int input)
    {
        TotalXP += input;
    }

    public int GetXP()
    { return TotalXP; }
}
