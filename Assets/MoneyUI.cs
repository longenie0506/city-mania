using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoneyUI : MonoBehaviour
{
    public PlayerController player;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI levelText;
    public Slider expSlider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(player && moneyText && levelText && expSlider){
            int moneyvalue = (int)player.getCurrentMoney();
            moneyText.text = moneyvalue.ToString();
            levelText.text = player.getLevel().ToString();
            expSlider.value = player.getCurrentExp()/player.getMaxExp();
        }
    }
}
