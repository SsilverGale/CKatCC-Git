using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTree : MonoBehaviour
{
    [SerializeField] GameObject treePanel;
    [SerializeField] string Class;
    [SerializeField] string skillName;
    [SerializeField] GameObject speedsterSkills;
    [SerializeField] GameObject sniperSkills;
    [SerializeField] GameObject tankSkills;
    [SerializeField] GameObject supportSkills;
    GameObject Player;
    UI ui;
    bool enoughXP = false;
    bool isPress = false;


    void Start()
    {
        ui = GameObject.FindGameObjectWithTag("UI").GetComponent<UI>();
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            isPress = true;
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            isPress = false;
        }
        if (Player == null) 
        {
            Player = GameObject.FindGameObjectWithTag("Player");
        }
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
            enoughXP = true;
        }
        else
        {
            enoughXP = false;
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
                tankSkills.SetActive(true);
            }
            if (Class == "SniperPlayer(Clone)")
            {
                sniperSkills.SetActive(true);
            }
            if (Class == "SupportPlayer(Clone)")
            {
                supportSkills.SetActive(true);
            }
            if (Class == "SpeedsterPlayer(Clone)")
            {
                speedsterSkills.SetActive(true);
            }
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.parent.name == "TankPlayer(Clone)" || other.gameObject.transform.parent.name == "SpeedsterPlayer(Clone)" || other.gameObject.transform.parent.name == "SupportPlayer(Clone)" || other.gameObject.transform.parent.name == "SniperPlayer(Clone)")
        {
            ui.ActivatePopup(true);
        }           
        
    }

    private void OnTriggerStay(Collider other)
    {
        if ((other.gameObject.transform.parent.name == "TankPlayer(Clone)" || other.gameObject.transform.parent.name == "SpeedsterPlayer(Clone)" || other.gameObject.transform.parent.name == "SupportPlayer(Clone)" || other.gameObject.transform.parent.name == "SniperPlayer(Clone)") && isPress)
        {
            LockAbility(false);
            Class = Player.transform.parent.name;
            MouseClamp(false);
            PanelActivate(true);
            SetClass();
        }
    }


    void OnTriggerExit(Collider other)
    {
        ui.ActivatePopup(false);
        if (other.gameObject.transform.parent.name == "TankPlayer(Clone)" || other.gameObject.transform.parent.name == "SpeedsterPlayer(Clone)" || other.gameObject.transform.parent.name == "SupportPlayer(Clone)" || other.gameObject.transform.parent.name == "SniperPlayer(Clone)")
        {
            
            LockAbility(true);
            MouseClamp(true);
            PanelActivate(false);
        }
    }

    public bool GetEnoughXP()
    {
        return enoughXP;
    }

    public void updateEnoughXP(bool input)
    {
        enoughXP = input;
    }
}
