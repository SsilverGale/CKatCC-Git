using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetThisActiveTIme : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("SetActiveThis", 3);
    }

    public void SetActiveThis()
    {
        GetComponent<Button>().interactable = true;
    }
}
