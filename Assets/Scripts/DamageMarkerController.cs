using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageMarkerController : MonoBehaviour
{

    private Text myText;

    [SerializeField]
    private float moveAmt;
    [SerializeField]
    private float moveSpeed;

    private Vector3 moveDir;
    private bool canMove = false;
    

    // Start is called before the first frame update
    void Start()
    {
        moveDir = transform.up;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + moveDir,
                moveAmt * (moveSpeed * Time.deltaTime));
        }
    }

    public void SetTextAndMove(string dmg)
    {
        myText = GetComponentInChildren<Text>();
        myText.color = Color.red;
        myText.text = dmg;
        canMove = true;
    }
}
