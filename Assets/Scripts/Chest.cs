
using UnityEngine;

public class Chest : Collectable
{

    public Sprite emptyChest;
    public int moneyAmount = 25;



    protected override void OnCollect()
    {


        if(!collected){

        collected = true;
        GetComponent<SpriteRenderer>().sprite = emptyChest;
        GameManager.instance.money += moneyAmount;

        //show text
        GameManager.instance.ShowText("+" + moneyAmount + " coins", 24, Color.yellow,transform.position,Vector3.up * 25, 1.5f);

        }

    }



}
