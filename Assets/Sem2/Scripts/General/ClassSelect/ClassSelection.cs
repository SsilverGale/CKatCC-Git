using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClassSelection : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] GameObject SniperClassObject;
    [SerializeField] GameObject TankClassObject;
    [SerializeField] GameObject SpeedsterClassObject;
    [SerializeField] Transform Spawnpoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChooseSniper()
    {
        Instantiate(SniperClassObject,Spawnpoint.position,Quaternion.identity);
        ClosePanel();
    }

    public void ChooseTank()
    {
        Instantiate(TankClassObject, Spawnpoint.position, Quaternion.identity);
        ClosePanel();
    }

    public void ChooseSpeedster()
    {
        Instantiate(SpeedsterClassObject, Spawnpoint.position, Quaternion.identity);
        ClosePanel();
    }

    void ClosePanel()
    {
        panel.SetActive(false);
    }



}
