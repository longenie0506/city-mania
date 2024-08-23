using System;
using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    // ------ Controller 
    public float speed = 0;
    private Rigidbody rb;
    [SerializeField] private FixedJoystick joystick;
    private float xInput;
    private float yInput;

    // ------ Gameplay
    // --- Player
    private int currentLevel = 1;
    private int maxLevel = 5;
    private float currentExp = 0;
    private float maxExp = 20;
    private float currentMoney = 0;
    private float maxMoney = 99999999;

    // --- Coffee
    private bool isCoffee=false; 
    [SerializeField] private GameObject coffeeObject;
    private GameObject followObject;

    // Building
    public GameObject emptySpaceUI;
    public TextMeshProUGUI costBuildingText;
    public TextMeshProUGUI levelBuildingText;
    public Button buyButton;
    private EmptySpace currentEmptySpace;
    private Staff currentStaff;

    // Master gameplay
    [SerializeField] private GamePlayManager gamePlayManager;

    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if(emptySpaceUI){
            emptySpaceUI.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //xInput = Input.GetAxis("Horizontal")>0?1:Input.GetAxis("Horizontal")<0?-1:0;
        ///yInput = Input.GetAxis("Vertical")>0?1:Input.GetAxis("Vertical")<0?-1:0;
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
            if(coffeeObject){
                Vector3 spawnLocation = new Vector3(transform.position.x,transform.position.y+10,transform.position.z);
                followObject = Instantiate(coffeeObject,spawnLocation,Quaternion.identity);
                FollowComponent followComponent = followObject.GetComponent<FollowComponent>();
                followComponent.SetObjectTarget(this.gameObject);
            }
        }
        
        // Coffee delivered
        if(collider.CompareTag("Portal")){
            ShopGameplay shopGameplay = collider.GetComponentInParent<ShopGameplay>();
            
            if(isCoffee){
                isCoffee = false;
                if(followObject){
                    Destroy(followObject);
                }
                if(shopGameplay){
                    shopGameplay.portalTrigger();
                }
            }

            if(shopGameplay.getCurrentMaxStack() > 0 && shopGameplay){
                updateResource(shopGameplay.getCurrentMaxStack(),shopGameplay.expEarn);
                shopGameplay.setCurrentMaxStack(0f);
            }
        }

        // Build new building
        if(collider.CompareTag("EmptySpace")){
            currentEmptySpace = collider.GetComponent<EmptySpace>();
            if(emptySpaceUI && costBuildingText && levelBuildingText && buyButton && currentEmptySpace){
                emptySpaceUI.gameObject.SetActive(true);
                costBuildingText.text = currentEmptySpace.getCostRequire().ToString();
                levelBuildingText.text = currentEmptySpace.getLevelRequire().ToString();
                if(currentMoney>=currentEmptySpace.getCostRequire() && currentLevel>=currentEmptySpace.getLevelRequire()){
                    buyButton.onClick.RemoveAllListeners();
                    buyButton.gameObject.SetActive(true);
                    buyButton.onClick.AddListener(buyButtonEvent);
                }else{
                    buyButton.gameObject.SetActive(false);
                }
            }
        }

        if(collider.CompareTag("Staff")){
            currentStaff = collider.GetComponent<Staff>();
            if(emptySpaceUI && costBuildingText && levelBuildingText && buyButton && currentStaff && !currentStaff.getIsBought()){
                emptySpaceUI.gameObject.SetActive(true);
                costBuildingText.text = currentStaff.getCostRequire().ToString();
                levelBuildingText.text = currentStaff.getLevelRequire().ToString();
                if(currentMoney>=currentStaff.getCostRequire() && currentLevel>=currentStaff.getLevelRequire()){
                    buyButton.onClick.RemoveAllListeners();
                    buyButton.gameObject.SetActive(true);
                    buyButton.onClick.AddListener(buyStaffButtonEvent);
                }else{
                    buyButton.gameObject.SetActive(false);
                }
            }
        }
    }

    private void OnTriggerExit(Collider collider){
        if(collider.CompareTag("EmptySpace")){
            currentEmptySpace = collider.GetComponent<EmptySpace>();
            if(emptySpaceUI && costBuildingText && levelBuildingText && buyButton && currentEmptySpace){
                emptySpaceUI.gameObject.SetActive(false);
            }
        }

        if(collider.CompareTag("Staff")){
            currentStaff = collider.GetComponent<Staff>();
            
            if(emptySpaceUI && costBuildingText && levelBuildingText && buyButton && currentStaff){
                emptySpaceUI.gameObject.SetActive(false);
            }
        }
    }


    private void buyButtonEvent(){
        if(emptySpaceUI && costBuildingText && levelBuildingText && buyButton && currentEmptySpace){
            currentMoney -= currentEmptySpace.getCostRequire();
            currentEmptySpace.buyBuilding();
            emptySpaceUI.gameObject.SetActive(false);
        }
    }

    private void buyStaffButtonEvent(){
        if(emptySpaceUI && costBuildingText && levelBuildingText && buyButton && currentStaff){
            currentMoney -= currentEmptySpace.getCostRequire();
            currentStaff.setIsBought(true);
            ShopGameplay shopGameplay = currentStaff.GetComponentInParent<ShopGameplay>();
            shopGameplay.setIsStaff(true);
            emptySpaceUI.gameObject.SetActive(false);
        }
    }

    private void updateResource(float money=0,float exp=0){
        currentMoney += money;
        currentExp += exp;

        // Check resource over-limit
        if(currentMoney>maxMoney){
            currentMoney = maxMoney;
        }
        if(currentExp>=maxExp && currentLevel<maxLevel){
            currentLevel += 1;
            currentExp -= maxExp;
            changeMaxLevel();
        }else if(currentExp>=maxExp && currentLevel>=maxLevel){
            currentExp = maxExp;
        }

    }

    private void changeMaxLevel(){
        switch (currentLevel)
        {
            case 1: 
                maxExp = 100; //100
                break;
            case 2: 
                maxExp = 500; //500
                break;
            case 3: 
                maxExp = 1200;//1200
                break;
            case 4: 
                maxExp = 2500;//2500
                break;
            case 5: 
                maxExp = 5000;//5000
                break;
            default:
                maxExp = 100;//100
                break;
        }
        if(gamePlayManager){
            gamePlayManager.LevelUp();
        }
    }

    public float getCurrentMoney(){
        return currentMoney;
    }

    public float getCurrentExp(){
        return currentExp;
    }

    public float getMaxExp(){
        return maxExp;
    }

    public float getLevel(){
        return currentLevel;
    }
}
