using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptySpace : MonoBehaviour
{
    private GameObject spawnBuilding;
    private float costRequire;
    private int levelRequire;

    // Start is called before the first frame update
    void Start()
    {
        if(spawnBuilding){
            ShopGameplay shop = spawnBuilding.GetComponent<ShopGameplay>();
            costRequire = shop.cost;
            levelRequire = shop.unlockLevel;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public float getCostRequire(){
        return costRequire;
    }

    public int getLevelRequire(){
        return levelRequire;
    }

    public void setSpawnBuilding(GameObject spawnBuilding){
        this.spawnBuilding = spawnBuilding;
    }

    public void setCostRequire(float costRequire){
        this.costRequire = costRequire;
    }

    public void setLevelRequire(int levelRequire){
        this.levelRequire = levelRequire;
    }


    public void buyBuilding(){
        if(spawnBuilding){
            spawnBuilding.SetActive(true);
            Destroy(this.gameObject);
        }
    }


}
