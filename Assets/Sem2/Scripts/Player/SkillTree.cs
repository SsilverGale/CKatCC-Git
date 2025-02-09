using Mirror.BouncyCastle.Asn1.Esf;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTree : MonoBehaviour
{
    [SerializeField] GameObject treePanel;
    [SerializeField] string Class;
    [SerializeField] string skillName;
    [SerializeField] GameObject[] speedsterSkills;
    [SerializeField] GameObject[] sniperSkills;
    [SerializeField] GameObject[] tankSkills;
    [SerializeField] GameObject[] supportSkills;
    XP xpGen; 
    GameObject Player;


    void Start()
    {
        xpGen = GameObject.FindGameObjectWithTag("XPHolder").GetComponent<XP>();
    }
    public void PanelActivate(bool input)
    {
        treePanel.SetActive(input);
    }

    public void ActivateSkill(string inputN, int inputC)
    {
        if (inputC >= Player.GetComponent<PlayerXP>().GetPlayerXP())
        {
            Player.GetComponent<PlayerXP>().IncrementXP(-inputC);
            skillName = inputN;
            if (Class == "TankPlayer")
            {
                //Player.GetComponent<TankAbilities>().LevelSkill(inputN);
            }
            if (Class == "SniperPlayer")
            {
                // Player.GetComponent<SniperAbilities>().LevelSkill(inputN);
            }
            if (Class == "SupportPlayer")
            {
                //Player.GetComponent<SupportAbilities>().LevelSkill(inputN);
            }
            if (Class == "SpeedsterPlayer")
            {
                //Player.GetComponent<SpeedsterAbilities>().LevelSkill(inputN);
            }
        }
        else
        {
            Debug.Log("Not Enough XP!");
        }
    }

    public void SetClass()
    {
        Class = gameObject.transform.parent.name;
        if (Class == "TankPlayer")
        {
            for (int i = 0; i < tankSkills.Length; i++) 
            {tankSkills[i].SetActive(true);}
            
        }
        if (Class == "SniperPlayer")
        {
            for (int i = 0; i < sniperSkills.Length; i++)
            { sniperSkills[i].SetActive(true); }
        }
        if (Class == "SupportPlayer")
        {
            for (int i = 0; i < supportSkills.Length; i++)
            { supportSkills[i].SetActive(true); }
        }
        if (Class == "SpeedsterPlayer")
        {
            for (int i = 0; i < speedsterSkills.Length; i++)
            { speedsterSkills[i].SetActive(true); }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "TankPlayer" || other.gameObject.name == "SpeedsterPlayer "|| other.gameObject.name == "SupportPlayer"|| other.gameObject.name == "TankPlayer")
        {
            PanelActivate(true);
            Class = other.gameObject.name;
            Player = other.gameObject;
        }
    }
}
