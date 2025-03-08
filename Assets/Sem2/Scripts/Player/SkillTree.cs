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
    GameObject Player;
    UI ui;


    void Start()
    {
        ui = GameObject.FindGameObjectWithTag("UI").GetComponent<UI>();
    }
    public void PanelActivate(bool input)
    {
        treePanel.SetActive(input);
    }

    public void SetSkillName(string inputN)
    {
        skillName = inputN;
    }

    public void MouseClamp(bool input)
    {
        Player.transform.GetChild(1).GetComponent<PlayerLook>().MouseClamp(input);
    }

    public void ActivateSkill(int inputC)
    {
        if (inputC <= Player.GetComponent<PlayerXP>().GetPlayerXP())
        {
            Player.GetComponent<PlayerXP>().IncrementXP(-inputC);
            if (Class == "TankPlayer(Clone)")
            {
                Player.GetComponent<TankAbilities>().LevelSkill(skillName);
            }
            if (Class == "SniperPlayer(Clone)")
            {
                Player.GetComponent<SniperAbilities>().LevelSkill(skillName);
            }
            if (Class == "SupportPlayer(Clone)")
            {
               Player.GetComponent<SupportAbilities>().LevelSkill(skillName);
            }
            if (Class == "SpeedsterPlayer(Clone)")
            {
               Player.GetComponent<SpeedsterAbilities>().LevelSkill(skillName);
            }
        }
        else
        {
            Debug.Log("Not Enough XP!");
        }
        ui.UpdateXP();
    }

    public void LockAbility(bool input)
    {
        if (Class == "TankPlayer(Clone)")
        {
            Player.GetComponent<TankAbilities>().enabled = input;
        }
        if (Class == "SniperPlayer(Clone)")
        {
            Player.GetComponent<SniperAbilities>().enabled = input;
        }
        if (Class == "SupportPlayer(Clone)")
        {
            Player.GetComponent<SupportAbilities>().enabled = input;
        }
        if (Class == "SpeedsterPlayer(Clone)")
        {
            Player.GetComponent<SpeedsterAbilities>().enabled = input;
        }
    }

    public void SetClass()
    {
        if (Class == "TankPlayer(Clone)")
        {
            for (int i = 0; i < tankSkills.Length; i++)
            { tankSkills[i].SetActive(true); }
        }
        if (Class == "SniperPlayer(Clone)")
        {
            for (int i = 0; i < sniperSkills.Length; i++)
            { sniperSkills[i].SetActive(true); }
        }
        if (Class == "SupportPlayer(Clone)")
        {
            for (int i = 0; i < supportSkills.Length; i++)
            { supportSkills[i].SetActive(true); }
        }
        if (Class == "SpeedsterPlayer(Clone)")
        {
            for (int i = 0; i < speedsterSkills.Length; i++)
            { speedsterSkills[i].SetActive(true); }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.parent.name == "TankPlayer(Clone)" || other.gameObject.transform.parent.name == "SpeedsterPlayer(Clone)" || other.gameObject.transform.parent.name == "SupportPlayer(Clone)" || other.gameObject.transform.parent.name == "SniperPlayer(Clone)")
        {
            LockAbility(false);
            Class = other.gameObject.transform.parent.name;
            Player = other.gameObject;
            MouseClamp(false);
            PanelActivate(true);
            SetClass();
            
        }
    }
        
    void OnTriggerExit(Collider other)
    {
        Debug.Log(other.gameObject.transform.parent.name);
        if (other.gameObject.transform.parent.name == "TankPlayer(Clone)" || other.gameObject.transform.parent.name == "SpeedsterPlayer(Clone)" || other.gameObject.transform.parent.name == "SupportPlayer(Clone)" || other.gameObject.transform.parent.name == "SniperPlayer(Clone)")
        {
            LockAbility(true);
            MouseClamp(true);
            PanelActivate(false);            
        }
    }

    
}
