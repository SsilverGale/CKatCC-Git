using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] Texture2D Ketchup;
    [SerializeField] Texture2D Relish;
    [SerializeField] Texture2D Mustard;
    [SerializeField] GameObject Holder;
    [SerializeField] Text Ammotext;
    [SerializeField] Slider slider;
    [SerializeField] Text waveCountText;
    [SerializeField] Text xpText;
    [SerializeField] Text waveCountdownText;
    [SerializeField] GameObject[] classAbilityUI;
    [SerializeField] Button[] tankButtons;
    [SerializeField] Button[] sniperButtons;
    [SerializeField] Button[] speedsterButtons;
    [SerializeField] Button shift;
    [SerializeField] Button E;
    [SerializeField] GameObject hitMarker;
    PlayerHealth hp;
    PlayerXP xp;
    WaveSpawn waveSpawn;
    int dashCount = 0;
    [SerializeField] Text dashText;
    public int maxDashCount = 0;
    public float dashReload = 0;

    bool isWavePaused = false;
    bool first = false;

    string Class;
    bool canShoot;
    int maxAmmo;
    int currentAmmo;
    float time;

    int MustardCount = 0;

    bool isClick = false;

    bool MustardReload = false;

    public bool shiftEnabled = false;

    bool eEnabled = false;

    bool activateShift = false;

    bool activateE = false;

    public bool abilityShiftActive = false;

    bool abilityEActive = false;

    public GameObject popup;

    // Start is called before the first frame update
    void Start()
    {
        waveSpawn = GameObject.FindWithTag("WaveSpawn").GetComponent<WaveSpawn>();
        xp = GameObject.FindWithTag("Player").GetComponent<PlayerXP>();
        hp = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isWavePaused)
        {           
            waveCountdownText.text = "Next Wave: " + waveSpawn.GetPauseTime().ToString();
        }
        
        if (hp == null)
        {
            xp = GameObject.FindWithTag("Player").GetComponent<PlayerXP>();
            hp = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();           
        }
        if (isClick) 
        {
            slider.value = hp.getHp() / hp.GetMaxHP();
        }          
        if (isClick)
        {
            Ammotext.text = "Ammo: " + currentAmmo.ToString() + "/" + maxAmmo.ToString();
            if (Class.Equals("Relish"))
            {
                Ammotext.text = "Ammo: " + "-/-";
            }
            if (Input.GetKeyDown(KeyCode.R) || currentAmmo == 0 && !first)
            {
                first = true;
                StartCoroutine(Reload(time));
                MustardReload = true;
            }
        }

        if (shiftEnabled && abilityShiftActive)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                shift.GetComponent<Button>().interactable = false;
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                shift.GetComponent<Button>().interactable = true;
            }
        }

        if (eEnabled && abilityEActive)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                E.GetComponent<Button>().interactable = false;
            }

            if (Input.GetKeyUp(KeyCode.E))
            {
                E.GetComponent<Button>().interactable = true;
            }
        }
        if (MustardCount < maxAmmo && MustardReload && Class == "Mustard")
        {
            SoundManager.PlaySound(SoundType.TANKRELOAD);
            MustardCount++;
            StartCoroutine(WaitSound());
        }
        if (Class == "Ketchup")
        {
           dashText.text = dashCount.ToString();
        }
    }

    public void UpdateXP()
    {
        xpText.text = "XP: " + xp.GetPlayerXP().ToString();
    }

    public void setRelish()
    {
        Holder.GetComponent<RawImage>().texture = Relish;
        shift = sniperButtons[0];
        E = sniperButtons[1];
        classAbilityUI[1].SetActive(true);
        Class = "Relish";
        time = 3f;

    }
    public void setKetchup()
    {
        Holder.GetComponent<RawImage>().texture = Ketchup;
        shift = speedsterButtons[0];
        E = speedsterButtons[1];
        classAbilityUI[0].SetActive(true);
        maxAmmo = 350;
        Class = "Ketchup";
        time = 1.5f;
        currentAmmo = maxAmmo;
    }

    public void setMustard()
    {
        Holder.GetComponent<RawImage>().texture = Mustard;
        shift = tankButtons[0];
        E = tankButtons[1];
        abilityShiftActive = true;
        shiftEnabled = true;
        SetShift(true);
        classAbilityUI[2].SetActive(true);
        maxAmmo = 6;
        Class = "Mustard";
        time = 0.75f;
        currentAmmo = maxAmmo;
    }

    public void decreaseAmmo(int amount)
    {
        currentAmmo -= amount;
    }

    IEnumerator Reload(float time)
    {   
        if (Class == "Mustard")
        {
            MustardCount = currentAmmo;
            MustardReload = true;
            yield return new WaitForSeconds((maxAmmo - currentAmmo) * time);       
            MustardReload = false;
        }
        else
        {
            yield return new WaitForSeconds(time);
        }
        if (Class == "Mustard")
        {
            SoundManager.PlaySound(SoundType.CHK);
        }
        currentAmmo = maxAmmo;
        first = false;
    }

    public int returnAmmo()
    {
        return currentAmmo;
    }

    public void enableIsclick()
    {
        isClick = true;
    }

    IEnumerator WaitSound()
    {
        MustardReload = false;
        yield return new WaitForSeconds(0.75f);
        MustardReload = true;
    }

    public void UpdateWaveCount(int wave)
    {
        waveCountText.text = "Wave: " + wave;
    }

    public void PauseWave(bool input)
    {
        waveCountdownText.gameObject.SetActive(input);
        isWavePaused = input;
    }

    public void SetShift(bool input)
    {
        shiftEnabled = input;
        abilityShiftActive = input;
    }

    public void SetE(bool input)
    {
        eEnabled = input;
        abilityEActive = input;
    }

    public void UpdateDash(int input)
    {
        dashCount += input;
    }

    public void HitMarker()
    {
        SoundManager.PlaySound(SoundType.HM);
        hitMarker.SetActive(true);
        Invoke("DisableHitMarker", 0.3f);
    }

    public void DisableHitMarker()
    {
        hitMarker.SetActive(false);
    }

    public int GetMaxAmmo()
    {
        return maxAmmo;
    }

    public void ActivatePopup(bool input)
    {
        popup.gameObject.SetActive(input);
    }
}
