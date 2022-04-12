using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDamageUI : MonoBehaviour
{
    public GameObject enemyDamageUI;
    public Text damageText;
    
    
    public void SetValue(float dmg)
    {
        Debug.Log("SET DAMAGE VALUE");
        damageText.text = dmg.ToString();
    }

    public void showDamage()
    {
        enemyDamageUI.SetActive(true);
        Invoke("NoShowDamage", 0.5f);
    }

    void NoShowDamage()
    {
        damageText.text = "";
    }
}
