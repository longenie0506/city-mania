using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowComponent : MonoBehaviour
{
    [SerializeField] private GameObject target;
    public Vector3 targetOffset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(target){
            transform.position = new Vector3(target.transform.position.x+targetOffset.x,target.transform.position.y+targetOffset.y,target.transform.position.z+targetOffset.z);
        }
    }

    public void SetObjectTarget(GameObject gameObject){
        this.target = gameObject;
    }
}
