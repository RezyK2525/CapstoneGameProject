using System;
using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

namespace UnityStandardAssets.Characters.FirstPerson
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(CapsuleCollider))]
    public class BetterPlayerMovement : Fighter
    {
        
        [Serializable]
        public class MovementSettings
        {
            public string yAxisInput = "Vertical";
            public string xAxisInput = "Horizontal";
            public string inputMouseX = "Mouse X";
            public string inputMouseY = "Mouse Y";
            public string jumpButton = "Jump";
            public float mouseSensitivity = 1f;
            public float groundAcceleration = 100f;
            public float airAcceleration = 100f;
            public float groundLimit = 12f;
            public float airLimit = 1f;
            public float gravity = 16f;
            public float friction = 6f;
            public float jumpHeight = 6f;
            public float rampSlideLimit = 5f;
            public float slopeLimit = 45f;
            public bool additiveJump = true;
            public bool autoJump = true;
            public bool clampGroundSpeed = false;
            public bool disableBunnyHopping = false;
            public KeyCode RunKey = KeyCode.LeftShift;
            public float RunMultiplier = 2.0f;
    
        }
        public MovementSettings movementSettings = new MovementSettings();
        
        //Other movement Vars
        private Vector3 vel;
        private Vector3 inputDir;
        private Vector3 _inputRot;
        private Vector3 groundNormal;

        private bool onGround = false;
        private bool jumpPending = false;
        private bool ableToJump = true;
        internal bool isRunning = false;
        
        public Vector3 InputRot { get => _inputRot; }
        
        
        
        // Display HUD 
        [Serializable]
        public class HUDsettings
        {

            public StatusBar healthBar;
            public StatusBar manaBar;
        }

        public HUDsettings hudSettings = new HUDsettings();

        // Basic Settings
        //MADE MONEY INTERNAL???
        internal int money;
        internal int gainedMoney;

        public PhotonView view;

        internal bool cursorSwap;


        //Changed these to private??? they arent used???
        private bool isMoving;
        private bool inInteractRange;

        // Animator - Not internal, needs to be public
        public Animator anim;

        // Equipment Manager

        //Players Attack Settings
        private bool allowAttack = true;
        private float cooldown = 0.6f;

        private float lastSwing;
        //public float nextAttackTime = 0f;
        //public static int noOfClicks = 0;
        //float maxComboDelay = 1;

        private Rigidbody rb;
        private void Start()
        {
            // TODO: Need to add method here to put set character stats

            //m_RigidBody = GetComponent<Rigidbody>();

            //DONT NEED THIS MAYBE????
            //m_Capsule = GetComponent<CapsuleCollider>();
            
            rb = GetComponent<Rigidbody>();

            // Lock cursor
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;


            view = GetComponent<PhotonView>();
            if (!GameManager.instance.isMultiplayer || view.IsMine)
            {

                //= THIS DOESNT WORK NEEDS FIX!
                GameManager.instance.player = this;




                GameManager.instance.cam.GetComponent<SmoothPlayerCamera>().player = this;
                GameManager.instance.player.GetComponent<SpellUser>().fpCam = GameManager.instance.cam;
                

                List<StatusBar> bars = new List<StatusBar>(FindObjectsOfType<StatusBar>());
                hudSettings.healthBar = bars.Where(bar => bar.name == "HealthBar").First();
                hudSettings.manaBar = bars.Where(bar => bar.name == "ManaBar").First();
                hudSettings.healthBar.SetMax(stats.maxHP);
                hudSettings.manaBar.SetMax(stats.maxMana);

                /*
                cam.transform.position = transform.position + new Vector3(0f, 1.287f, 0.192f);
                cam.transform.parent = transform;

                mouseLook.Init (transform, cam.transform);
                */
            }

            DontDestroyOnLoad(gameObject);
        }

        public void cursorState()
        {
            cursorSwap = !cursorSwap;

            if (cursorSwap)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Confined;
            }
            else
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }

        }

        private void Update()
        {
            // Debug.Log(vel);
            //MouseLook();
            //GetMovementInput();
            //Debug.Log("GettingCalled");
            if (!GameManager.instance.isMultiplayer || view.IsMine)
            {
                // Debug.Log("GettingCalled");
                MouseLook();
                GetMovementInput();

                //FIXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX THIS
                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //anim.SetFloat("vertical", Input.GetAxis("Vertical"));
                //anim.SetFloat("horizontal", Input.GetAxis("Horizontal"));

                /*  COMBO ANIMATION -- 
                if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
                {
                    anim.SetBool("Attack1", false);
                }
                if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("Attack2"))
                {
                    anim.SetBool("Attack2", false);
                }
                if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("Attack3"))
                {
                    anim.SetBool("Attack3", false);
                    noOfClicks = 0;
                }

                if(Time.time - lastSwing > maxComboDelay)
                {
                    noOfClicks = 0;
                }

                if(Time.time > nextAttackTime)
                {
                */

                if (Input.GetMouseButton(0))
                {
                    if (allowAttack)
                    {
                        if (Time.time - lastSwing > cooldown)
                        {
                            Attack();
                        }
                    }
                }


                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    Debug.Break();
                }

            }
        }

        // Attack //===================================
        void Attack()
        {
            lastSwing = Time.time;
            //noOfClicks++;
            // user attack

            //if (noOfClicks == 1)
            //{

            if (EquipmentManager.instance.currentEquipment[0] != null)
            {
                EquipmentManager.instance.weaponHolder.GetChild(0).gameObject.GetComponent<BoxCollider>().enabled =
                    true;
                anim.SetTrigger("attack");
                //EquipmentManager.instance.weaponHolder.GetChild(0).gameObject.GetComponent<BoxCollider>().enabled = false;
            }
            // Else add punch here

            //}
            /*noOfClicks = Mathf.Clamp(noOfClicks, 0, 3);

            if(noOfClicks >= 2 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
            {
                anim.SetBool("Attack1", false);
                anim.SetBool("Attack2", true);
            }

            if (noOfClicks >= 3 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("Attack2"))
            {
                anim.SetBool("Attack2", false);
                anim.SetBool("Attack3", true);
            }
            */
        }

        // Not implemented - need to fix this spaguett
        public void EndAnimationFunc(string message)
        {
            if (message.Equals("AttackAnimationEnded"))
            {
                EquipmentManager.instance.weaponHolder.GetChild(0).gameObject.GetComponent<BoxCollider>().enabled =
                    false;
                //pc_attacking = false;
                //pc_anim.SetBool("attack", false);
                // Do other things based on an attack ending.
            }
        }
        
        public void setAllowedToAttack(bool maybe)
        {
            allowAttack = maybe;
        }

        // ------- // ===================================
        
    private void FixedUpdate() {
        vel = rb.velocity; 

        // Clamp speed if bunnyhopping is disabled
        if (movementSettings.disableBunnyHopping && onGround) {
            if (vel.magnitude > movementSettings.groundLimit)
                vel = vel.normalized * movementSettings.groundLimit;
        }

        // Jump
        if (jumpPending && onGround) {
            Jump();
        }

        // We use air physics if moving upwards at high speed
        if (movementSettings.rampSlideLimit >= 0f && vel.y > movementSettings.rampSlideLimit)
            onGround = false;

        if (onGround) {
            // Rotate movement vector to match ground tangent
            inputDir = Vector3.Cross(Vector3.Cross(groundNormal, inputDir), groundNormal);

            GroundAccelerate();
            ApplyFriction();
        }
        else {
            ApplyGravity();
            AirAccelerate();
        }

        rb.velocity = vel;

        // Reset onGround before next collision checks
        onGround = false;
        groundNormal = Vector3.zero;
    }

    void GetMovementInput() {
        float x = Input.GetAxisRaw(movementSettings.xAxisInput);
        float z = Input.GetAxisRaw(movementSettings.yAxisInput);
        //Debug.Log(isRunning);
        
        if (Input.GetKey(movementSettings.RunKey))
        {
            
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }

        inputDir = transform.rotation * new Vector3(x, 0f, z).normalized;

        if (Input.GetButtonDown(movementSettings.jumpButton))
            jumpPending = true;

        if (Input.GetButtonUp(movementSettings.jumpButton))
            jumpPending = false;
    }

    void MouseLook() {
        _inputRot.y += Input.GetAxisRaw(movementSettings.inputMouseX) * movementSettings.mouseSensitivity;
        _inputRot.x -= Input.GetAxisRaw(movementSettings.inputMouseY) * movementSettings.mouseSensitivity;

        if (_inputRot.x > 90f)
            _inputRot.x = 90f;
        if (_inputRot.x < -90f)
            _inputRot.x = -90f;

        transform.rotation = Quaternion.Euler(0f, _inputRot.y, 0f);
    }

    private void GroundAccelerate() {
        float addSpeed = movementSettings.groundLimit - Vector3.Dot(vel, inputDir);

        if (addSpeed <= 0)
            return;

        float accelSpeed = movementSettings.groundAcceleration * Time.deltaTime;

        if (accelSpeed > addSpeed)
            accelSpeed = addSpeed;

        if (isRunning)
        {
            vel += accelSpeed * inputDir * movementSettings.RunMultiplier;
        }
        else
        {
            vel += accelSpeed * inputDir;
        }
        

        if (movementSettings.clampGroundSpeed) {
            if (vel.magnitude > movementSettings.groundLimit)
                vel = vel.normalized * movementSettings.groundLimit;
        }
    }

    private void AirAccelerate() {
        Vector3 hVel = vel;
        hVel.y = 0;

        float addSpeed =  movementSettings.airLimit - Vector3.Dot(hVel, inputDir);

        if (addSpeed <= 0)
            return;

        float accelSpeed = movementSettings.airAcceleration * Time.deltaTime;

        if (accelSpeed > addSpeed)
            accelSpeed = addSpeed;

        vel += accelSpeed * inputDir;
    }

    private void ApplyFriction() {
        vel *= Mathf.Clamp01(1 - Time.deltaTime * movementSettings.friction);
    }

    private void Jump() {
        if (!ableToJump)
            return;

        if (vel.y < 0f || !movementSettings.additiveJump)
            vel.y = 0f;

        vel.y += movementSettings.jumpHeight;
        onGround = false;

        if (!movementSettings.autoJump)
            jumpPending = false;

        StartCoroutine(JumpTimer());
    }

    

    private void ApplyGravity() {
        vel.y -= movementSettings.gravity * Time.deltaTime;
    }

    private void OnCollisionStay(Collision other) {
        // Check if any of the contacts has acceptable floor angle
        foreach (ContactPoint contact in other.contacts) {
            if (contact.normal.y > Mathf.Sin(movementSettings.slopeLimit * (Mathf.PI / 180f) + Mathf.PI/2f)) {
                groundNormal = contact.normal;
                onGround = true;
                return;
            }
        }
    }

    // This is for avoiding multiple consecutive jump commands before leaving ground
    private IEnumerator JumpTimer() {
        ableToJump = false;
        yield return new WaitForSeconds(0.1f);
        ableToJump = true;
    }
}
    
}
