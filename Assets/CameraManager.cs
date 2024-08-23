using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject followObject;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(followObject){
            transform.position = new Vector3(followObject.transform.position.x,transform.position.y,followObject.transform.position.z-20);
        }
    }
}
