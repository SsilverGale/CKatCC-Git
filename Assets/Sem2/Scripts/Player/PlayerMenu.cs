using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerMenu : MonoBehaviour
{
    
    [SerializeField] private GameObject MenuUI;

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            //Opens Menu After hitting escape
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
                MenuUI.SetActive(true);
            }
            else
            {
                //Closes Menu After hitting escape
                Cursor.lockState = CursorLockMode.Locked;
                MenuUI.SetActive(false);
            }
                
        }
    }
}
