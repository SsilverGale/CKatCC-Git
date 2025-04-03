using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHPBar : MonoBehaviour
{
    [SerializeField] EnemyHealth health;
    [SerializeField] Scrollbar scrollbar;

    // Update is called once per frame
    void Update()
    {
        scrollbar.size = health.hp / 100;
    }
}
