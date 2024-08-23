using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AdaptivePerformance.VisualScripting;
using UnityEngine.UI;

public class ShopGameplay : MonoBehaviour
{
    private float timer = 0f;
    private bool purchasable = true;
    private bool isStaff = false;

    // Gameplay Ability
    public int unlockLevel = 1;
    public float cost = 0f;
    public float costOfStaff = 0f;
    public float moneyEarn = 1f;
    public float moneySecondLoop = 10f;
    public float moneyMaxStack = 100f;
    private float moneyCurrentStack = 0f;
    public float expEarn = 10f;
    private bool isActivated = false;
    public float durationActivated = 12f;
    private float endtimeActivated = 0f;
    private float moneyTimer = 0f; // start 
    private float moneyStackTimer = 0f;

    private Slider slider;
    public GameObject moneyQuad;
    public GameObject emptySpacePrefab;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponentInChildren<Slider>();
        slider.gameObject.SetActive(false);
        if(purchasable && emptySpacePrefab){
            GameObject spaceObject = Instantiate(emptySpacePrefab,transform.position, transform.rotation);
            EmptySpace emptySpace = spaceObject.GetComponent<EmptySpace>();
            Renderer prefabRenderer = gameObject.GetComponent<Renderer>();
            emptySpace.transform.localScale = new Vector3(prefabRenderer.bounds.size.x,emptySpace.transform.localScale.y,prefabRenderer.bounds.size.z);
            emptySpace.setSpawnBuilding(this.gameObject);
            emptySpace.setCostRequire(cost);
            emptySpace.setLevelRequire(unlockLevel);
            this.gameObject.SetActive(false);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer>endtimeActivated && endtimeActivated!=0){
            if(!isStaff){
                deactivateShop();
            }else{
                endtimeActivated+=durationActivated;
            }
            
        }
        if(isActivated){
            moneyTimer += Time.deltaTime;
            if(moneyTimer >= moneyStackTimer){
                stackMoney();
            }
            slider.value = (endtimeActivated-timer)/durationActivated;
        }
        if(moneyQuad && moneyCurrentStack>0){
            moneyQuad.SetActive(true);
        }else if(moneyQuad && moneyCurrentStack<=0){
            moneyQuad.SetActive(false);
        }
    }

    public float getCurrentMaxStack(){
        return moneyCurrentStack;
    }

    public void setCurrentMaxStack(float moneyCurrentStack){
        this.moneyCurrentStack = moneyCurrentStack;
    }

    public void portalTrigger(){
        activateShop();
    }

    public void activateShop(){

        // First time access
        if(!isActivated){
            moneyStackTimer += moneySecondLoop;
            isActivated = true;
            slider.gameObject.SetActive(true);
        }
        
        endtimeActivated = timer + durationActivated;
        
    }

    public void deactivateShop(){
        isActivated = false;
        endtimeActivated = 0;
        moneyTimer = 0f;
        moneyStackTimer = 0f;
        slider.gameObject.SetActive(false);
    }

    public void stackMoney(){
        moneyCurrentStack += moneyEarn;
        if(moneyCurrentStack > moneyMaxStack){
            moneyCurrentStack = moneyMaxStack;
        }
        moneyStackTimer += moneySecondLoop;
    }

    public void setPurchasable(bool purchasable){
        this.purchasable = purchasable;
    }

    public void setIsStaff(bool isStaff){
        this.isStaff = isStaff;
        // Start right when have staff
        if(isStaff){
            activateShop();
        }
    }
}
