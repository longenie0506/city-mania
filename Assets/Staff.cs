using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : MonoBehaviour
{
    private ShopGameplay shopGameplay;
    private float costRequire;
    private int levelRequire;
    private bool isBought = false;


    // Start is called before the first frame update
    void Start()
    {
        ShopGameplay shopGameplay = GetComponentInParent<ShopGameplay>();
        if(shopGameplay){
            costRequire = shopGameplay.costOfStaff;
            levelRequire = shopGameplay.unlockLevel;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(transform.position, Vector3.up, 40 * Time.deltaTime);
    }
    public float getCostRequire(){
        return costRequire;
    }

    public int getLevelRequire(){
        return levelRequire;
    }

    public bool getIsBought(){
        return isBought;
    }

    public void setIsBought(bool isBought){
        this.isBought = isBought;
    }
}
