using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChestScript : MonoBehaviour
{

    public int MoneyAmount = 25;
    public TextMeshProUGUI ChestText;
    
    
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("On Trigger Enter");
        if (other.tag == "Player")
        {
            //Debug.Log("Player hit trigger");
            //ChestText.text = "Press F to Open Chest!";
            //ChestText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("MONEYYYYYYYYYYYY");
            //ChestText.gameObject.SetActive(false);
            GameManager.instance.player.money += MoneyAmount;
        }
            
            
        
        
        
        
    }
}
