using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorIsHotsauce : MonoBehaviour
{
    bool isRising = false;
    bool isSinking = false;
    [SerializeField] float capturedTime = 0;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (isRising)
        {
            transform.position += Vector3.up * 0.01f;
        }
        if (isSinking)
        {
            transform.position += Vector3.down * 0.01f;
        }
        if (transform.position.y <= -6)
        {
            isSinking = false;
        }
        if (Time.time - capturedTime >= 5f && isRising)
        {
            isRising = false;
            StartCoroutine(EnableSink());
        }
    }

    public void StartRise()
    {
        Debug.Log("Floor is Hot Sauce!!!");
        StartCoroutine(EnableRise());
        capturedTime = Time.time;
    }

    IEnumerator EnableSink()
    {        
        yield return new WaitForSeconds(3);
        isSinking = true;
    }
    IEnumerator EnableRise()
    {
        yield return new WaitForSeconds(4.5f);
        isRising = true;
    }
}
