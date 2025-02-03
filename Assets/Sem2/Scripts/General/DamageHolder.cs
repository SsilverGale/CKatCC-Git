using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHolder : MonoBehaviour
{
    [SerializeField] float Damage;

    public float GetDamage()
    {
        return Damage;
    }
}
