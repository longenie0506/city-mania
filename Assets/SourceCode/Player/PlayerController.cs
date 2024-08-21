using System;
using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    private Rigidbody rb;
    [SerializeField] private FixedJoystick joystick;
    private float xInput;
    private float yInput;

    private bool isCoffee=false; 

    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        xInput = Input.GetAxis("Horizontal")>0?1:Input.GetAxis("Horizontal")<0?-1:0;
        yInput = Input.GetAxis("Vertical")>0?1:Input.GetAxis("Vertical")<0?-1:0;
    }

    private void FixedUpdate(){
        
        if(rb){
            rb.velocity = new Vector3(joystick.Horizontal*speed,rb.velocity.y,joystick.Vertical*speed);
        }   
        
        if(joystick){
            if(joystick.Horizontal != 0 || joystick.Vertical != 0){
                transform.rotation = Quaternion.LookRotation(new Vector3(rb.velocity.x, 0, rb.velocity.z));
            }
        }
        
    }

    private void OnTriggerEnter(Collider collider){
        
        // Take coffee
        if(collider.CompareTag("Portal-Coffee") && !isCoffee){
            isCoffee = true;
            Debug.Log(isCoffee);
        }
        
        // Coffee delivered
        if(collider.CompareTag("Portal") && isCoffee){
            isCoffee = false;
            Debug.Log(isCoffee);
        }
        
    }
}
