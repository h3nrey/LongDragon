using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using NaughtyAttributes;

public class PlayerBehaviour : MonoBehaviour
{
    static public PlayerBehaviour instance;
    [ReadOnly] public Vector2 input;
    [SerializeField] private float horizontalSpeed;

    [Header("Rotate")]
    [SerializeField] private float rotationSpeed;
    [ReadOnly] Vector3 inputVector;
    [ReadOnly] Vector3 currentPosition;

    [Header("Physics")]
    [SerializeField] public Rigidbody2D rb;
    [ReadOnly] public float velX, velY; // shorthands to rb.velocity in X and Y axis
    [SerializeField] public float maxVelocityY = 14;
    [SerializeField] public float gravityScale;

    [Header("Jump")]
    [SerializeField] float jumpForce = 300f;
    public float flySpeed;
    public float baseFlySpeed = 10f;
    public float maxFlySpeed = 20f;
    public float flySpeedIncrementer = 0.5f;

    [Header("Checks")]
    public bool facingRight = true;
    [ReadOnly] public bool pressingFlybutton = false;


    private void Awake() {
        if(!instance) instance = this;
    }


    private void FixedUpdate() {
        velX = rb.velocity.x;
        velY = rb.velocity.y;

        rb.velocity = input * horizontalSpeed * Time.fixedDeltaTime;
           

    }

    private void Update() {
        //Turn();
        //CheckFacing();
        Vector2 lookDir = input;


        float targetAngle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        rb.rotation = targetAngle;

        //print($"targetAngle: {targetAngle}, lookDir: {lookDir}");
    }

    #region Inputs
    public void GetInput(InputAction.CallbackContext context) {
        input = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context) {
        if (context.started)
            pressingFlybutton = true;
        if (context.canceled)
            pressingFlybutton = false;

        //if (context.performed) Flying();
    }
    #endregion

    //void CheckFacing() {
    //    if (input.x == 1) facingRight = true; else if(input.x == -1)facingRight = false;
    //}

    //void Turn() {
    //    if (facingRight) transform.rotation = Quaternion.Euler(0,0,transform.eulerAngles.z);
    //    else transform.rotation = Quaternion.Euler(0, 180, transform.eulerAngles.z);
    //}

    void Lean() {
        transform.eulerAngles = Vector3.forward * 90f;
        print("Leaning");
    }

}
