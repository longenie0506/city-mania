using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        LevelUp();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LevelUp(){
        if(playerController){
            
            for(int i=1;i<=playerController.getLevel();i++){
                GameObject[] lockArea = null;
                switch(i){
                    case 1:
                        lockArea = GameObject.FindGameObjectsWithTag("Level 1");
                        break;
                    case 2:
                        lockArea = GameObject.FindGameObjectsWithTag("Level 2");
                        break;
                    case 3:
                        lockArea = GameObject.FindGameObjectsWithTag("Level 3");
                        break;
                    case 4:
                        lockArea = GameObject.FindGameObjectsWithTag("Level 4");
                        break;
                    case 5:
                        lockArea = GameObject.FindGameObjectsWithTag("Level 5");
                        break;
                    default:
                        break;
                }
                if(lockArea != null){
                    foreach (GameObject area in lockArea)
                    {
                        area.gameObject.SetActive(false);
                    }
                }
            }
            
            
        }
    }
}
