using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
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
    
    }

    public MovementSettings movementSettings = new MovementSettings();
    private Rigidbody rb;
    
    private Vector3 vel;
    private Vector3 inputDir;
    private Vector3 _inputRot;
    private Vector3 groundNormal;

    private bool onGround = false;
    private bool jumpPending = false;
    private bool ableToJump = true;

    public Vector3 InputRot { get => _inputRot; }

    void Start() {
        rb = GetComponent<Rigidbody>();

        // Lock cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }


    private void Update() {
        MouseLook();
        GetMovementInput();
    }


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

        vel += accelSpeed * inputDir;

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