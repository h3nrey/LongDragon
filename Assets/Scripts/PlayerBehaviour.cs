using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using NaughtyAttributes;

public class PlayerBehaviour : MonoBehaviour
{
    static public PlayerBehaviour instance;
    [ReadOnly] public Vector2 input;
    [Header("Movement")]
    [SerializeField] private float baseSpeed;
    [SerializeField] private float speed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float speedIncrementer = 1;

    [Header("Rotate")]
    [SerializeField] private float rotationSpeed;
    [ReadOnly] Vector3 inputVector;
    [ReadOnly] Vector3 currentPosition;

    [Header("Physics")]
    [SerializeField] public Rigidbody2D rb;
    [ReadOnly] public float velX, velY; // shorthands to rb.velocity in X and Y axis
    [SerializeField] public float maxVelocityY = 14;
    [SerializeField] public float gravityScale;
    [SerializeField] float acceleration, decceleration;
    [SerializeField] float velPower;


    [Header("Jump")]
    [SerializeField] float jumpForce = 300f;
    public float flySpeed;
    public float baseFlySpeed = 10f;
    public float maxFlySpeed = 20f;
    public float flySpeedIncrementer = 0.5f;

    [Header("Checks")]
    public bool facingRight = true;
    [ReadOnly] public bool pressingFlybutton = false;
    [ReadOnly] public bool grounded = false;

    [Header("Body")]
    public List<Transform> segments;
    Vector2 lastDir = new Vector2(0,-1);

    [Header("Shoot")]
    [SerializeField] public GameObject fireProjectille;
    [SerializeField] public Transform canonPoint;
    [SerializeField] public float projecctilleSpeed;
    [SerializeField] public float projectilleCooldown;
    [ReadOnly] public bool canShoot = true;
    public List<GameObject> projectilles;

    private void Awake() {
        if(!instance) instance = this;
    }


    private void FixedUpdate() {
        velX = rb.velocity.x;
        velY = rb.velocity.y;

        if(input == Vector2.zero) {
            speed = baseSpeed;
            rb.gravityScale = gravityScale;
        }

        if(input.magnitude != 0) {
            rb.gravityScale = 0;
            rb.velocity = input * speed * Time.fixedDeltaTime;

            if(speed < maxSpeed) {
                speed += speedIncrementer;
            }

        }

        #region using force
        //Vector2 targetSpeed = input * horizontalSpeed;
        //Vector2 speedDif = targetSpeed - rb.velocity;
        //float accelRate = (Mathf.Abs(targetSpeed.sqrMagnitude) > 0.01f) ? acceleration : decceleration;
        //Vector2 movement = new Vector2(Mathf.Pow(Mathf.Abs(speedDif.x) * accelRate, velPower) * Mathf.Sign(speedDif.x), Mathf.Pow(Mathf.Abs(speedDif.y) * accelRate, velPower) * Mathf.Sign(speedDif.y));
        //rb.AddForce(movement * Time.fixedDeltaTime);
        #endregion


    }

    private void Update() {
        //Turn();
        //CheckFacing();
        Vector2 lookDir = new Vector2(input.x * 180, input.y * 90);


        //float targetAngle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        //rb.rotation = targetAngle;

        //float targetAngle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(new Vector3(0, lookDir.x, lookDir.y));
        Turn();

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

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Ground") grounded = true; else grounded = false;
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.tag == "Ground") grounded = false; else grounded = false;
    }

    void Turn() {
        Vector3 lookDir = new Vector3(0,0);
        if (input.magnitude != 0) lastDir = input ;

        float angle = Mathf.Atan2(lastDir.y, lastDir.x) * Mathf.Rad2Deg;
        lookDir = Vector3.forward * angle;
        Vector3 targetAngle = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, Mathf.LerpAngle(transform.eulerAngles.z, lookDir.z, rotationSpeed));
        transform.rotation = Quaternion.Euler(targetAngle);
        //print(targetAngle);s
        
        //if (lastDir == new Vector2(0, 1)) {
        //    lookDir = new Vector2(lastDir.x * 180, 90); //Up 
        //}
        //else if (lastDir == new Vector2(0, -1)) {
        //    lookDir = new Vector2(lastDir.x * 180, -90); //Down 
        //}
        //if (lastDir == new Vector2(1, 0)) {
        //lookDir = new Vector2(0, lastDir.y * 90); //Right 
        //}
        //else if (lastDir == new Vector2(-1, 0)) {
        //    lookDir = new Vector2(180, lastDir.y * 90); //Left 
        //}
        //else if (lastDir.x > 0 && lastDir.y > 0) {
        //    lookDir = new Vector2(0, 35); //up diagonal right 
        //}
        //else if (lastDir.x > 0 && lastDir.y < 0) {
        //    lookDir = new Vector2(0, -35); //down diagonal right 
        //}
        //else if (lastDir.x < 0 && lastDir.y > 0) {
        //    lookDir = new Vector2(180, 35); //up diagonal left 
        //}
        //else if (lastDir.x < 0 && lastDir.y < 0) {
        //    lookDir = new Vector2(180, -35); //down diagonal left 
        //}

        //Vector3 angle = new Vector3(0, Mathf.Lerp(transform.eulerAngles.y, lookDir.x, rotationSpeed),Mathf.Lerp(transform.eulerAngles.z, lookDir.y, rotationSpeed));
        //Quaternion targetRotation = Quaternion.Euler(angle);

        //transform.rotation = targetRotation;
    }

    void Lean() {
        transform.eulerAngles = Vector3.forward * 90f;
        print("Leaning");
    }

}
