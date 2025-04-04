using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aura : MonoBehaviour
{
    Player player;
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<DamageHolder>().SetDamage(player.GetComponent<TankAbilities>().ReturnAuraMod());
    }
}
