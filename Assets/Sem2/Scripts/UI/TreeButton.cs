using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeButton : MonoBehaviour
{
    [SerializeField] Button nextButton;

    public void EnableNext()
    {
        if (GameObject.FindWithTag("Shop").GetComponent<SkillTree>().GetEnoughXP() == true)
        {
            nextButton.interactable = true;
            this.GetComponent<Button>().enabled = false;
            GameObject.FindWithTag("Shop").GetComponent<SkillTree>().updateEnoughXP(false);
        }
        
    }
}
