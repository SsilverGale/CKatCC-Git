using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{

    [SerializeField] float mouseSensitivity;
    float xRotation;

    //the player model
    [SerializeField] Transform playerBody;
    // Start is called before the first frame update
    PlayerHealth hp;

    bool firstTime = true;
    void Start()
    {
        //locks the mouse so it isn't moving when the player is turning
        //added a condition to lock the mouse after first click so the player can clsass select for mini interactive project week 2 -Josh
        hp = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!hp.GetIsDowned())
        {
        if (Input.GetMouseButton(0) && firstTime) 
        {
                Cursor.lockState = CursorLockMode.Locked;
                firstTime = false;
        }
        //tracks the movement of the mouse
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * 250 * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * 250 * Time.deltaTime;

        //keeps track of the player's current rotation
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //rotates the player model based on what direction the player is looking
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
        }
    }

    public void MouseClamp(bool input)
    {
        if (input == true)
        {
            mouseSensitivity = 1f;
            Cursor.lockState = CursorLockMode.Locked;
        }
        if (input == false)
        {
            mouseSensitivity = 0f;
            Cursor.lockState = CursorLockMode.Confined;
        }
        

    }
}
