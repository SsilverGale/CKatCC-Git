using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    PlayerHealth hp;

    string Class;
    bool canShoot;
    int maxAmmo;
    int currentAmmo;
    float time;

    int MustardCount = 0;

    bool isClick = false;

    bool MustardReload = false;

    // Start is called before the first frame update
    void Start()
    {
            hp = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hp == null)
        {
                hp = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();           
        }
        if (isClick) 
        {
            slider.value = hp.getHp() / 100;
        }
            


        if (Input.GetMouseButtonDown(0))
        {
            Invoke("enableIsclick", 2f);
        }
        if (MustardCount >= (maxAmmo - currentAmmo))
        {
            MustardReload = false;
            MustardCount = 0;
        }
        if (MustardReload)
        {
            MustardCount++;
            SoundManager.PlaySound(SoundType.TANKRELOAD);
            MustardReload = false;
            StartCoroutine(WaitSound());
        }

        if (isClick)
        {
            Ammotext.text = "Ammo: " + currentAmmo.ToString() + "/" + maxAmmo.ToString();
            if (Class.Equals("Relish"))
            {
                Ammotext.text = "Ammo: " + "-/-";
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                StartCoroutine(Reload(time));

            }
        }
        
    }

    public void setRelish()
    {
        Holder.GetComponent<RawImage>().texture = Relish;
        Class = "Relish";
        time = 3f;

    }
    public void setKetchup()
    {
        Holder.GetComponent<RawImage>().texture = Ketchup;
        maxAmmo = 350;
        Class = "Ketchup";
        time = 1.5f;
        currentAmmo = maxAmmo;
    }

    public void setMustard()
    {
        Holder.GetComponent<RawImage>().texture = Mustard;
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
            MustardReload = true;
            yield return new WaitForSeconds((maxAmmo - currentAmmo) * time);
            MustardReload = false;
        }
        else
        {
            yield return new WaitForSeconds(time);
        }
        currentAmmo = maxAmmo;
    }

    public int returnAmmo()
    {
        return currentAmmo;
    }

    void enableIsclick()
    {
        isClick = true;
    }

    IEnumerator WaitSound()
    {
        yield return new WaitForSeconds(0.75f);
        MustardReload = true;
    }

    public void UpdateWaveCount(int wave)
    {
        waveCountText.text = "Wave: " + wave;
    }

}
