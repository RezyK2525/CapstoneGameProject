using UnityEngine;

public class Player : Mover
{
    private SpriteRenderer spriteRenderer;
    public Animator animator;
    public bool isMoving;
    private float cooldown = 0.5f;
    private float lastSwing;


    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
   
        DontDestroyOnLoad(gameObject);
        
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Time.time - lastSwing > cooldown)
            {
                lastSwing = Time.time;
                Swing();

            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            
        }
    }

    private void Swing()
    {
        animator.SetTrigger("Swing");
    }

    private void FixedUpdate(){

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if (x != 0 || y != 0)
        {

            //vector magnitude
            float mag = Mathf.Abs(x) + Mathf.Abs(y);

            animator.SetFloat("Horizontal", x);
            animator.SetFloat("Vertical", y);
            animator.SetFloat("Speed", mag);

            if (!isMoving)
            {
                isMoving = true;
                animator.SetBool("isMoving", isMoving);
            }
        }
        else
        {
            if (isMoving)
            {
                isMoving = false;
                animator.SetBool("isMoving", isMoving);
            }
        }
        
        UpdateMotor(new Vector3(x,y,0));

    }

    // For future character selection
    public void SwapSprite(int skinId)
    {
        spriteRenderer.sprite = GameManager.instance.playerSprites[skinId];

    }

    public void Heal(int healingAmount)
    {


        if (hitPoint == maxHitPoint)
        {
            return;
        }
        hitPoint += healingAmount;
        if (hitPoint > maxHitPoint)
        {
            hitPoint = maxHitPoint;
        }

        GameManager.instance.ShowText("+" + healingAmount.ToString() + "hp", 25, Color.green, transform.position, Vector3.up * 30, 1.0f);

    }

}
