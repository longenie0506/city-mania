using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalManager : MonoBehaviour
{
    private ShopGameplay shopGameplay;
    // Start is called before the first frame update
    void Start()
    {
        shopGameplay = GetComponentInParent<ShopGameplay>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // private void OnTriggerEnter(Collider collider){
    //     if(collider.CompareTag("Player")){
    //         shopGameplay.portalTrigger();
    //     }
    // }
}
