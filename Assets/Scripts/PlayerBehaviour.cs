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
    float yAngle = 0;
    float zAngle = 0;

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
    [SerializeField]Vector2 lastDir = new Vector2(0,0);

    [Header("Shoot")]
    [SerializeField] public GameObject fireProjectille;
    [SerializeField] public Transform canonPoint;
    [SerializeField] public float projecctilleSpeed;
    [SerializeField] public float projectilleCooldown;
    [ReadOnly] public bool canShoot = true;
    public List<GameObject> projectilles;

    [Header("Graphics")]
    public SpriteRenderer _graphics;

    private void Awake() {
        if(!instance) instance = this;
    }


    private void FixedUpdate() {
        velX = rb.velocity.x;
        velY = rb.velocity.y;

        if(Mathf.Abs(input.y)  > 0.01f || Mathf.Abs(input.x) > 0.01f) {
            rb.velocity = input * speed * Time.fixedDeltaTime;
        } else if(Mathf.Abs(input.y) < 0.01f || Mathf.Abs(input.x) < 0.01f) {
            rb.velocity = new Vector2(0, velY);
        }
    }

    private void Update() {
        Turn();

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
        if (input.magnitude != 0) lastDir = input;

        if (lastDir.x == 1) {
            yAngle = 0;
        }
        else if (lastDir.x == -1) yAngle = 180;

        if (Mathf.Abs(lastDir.y) > 0.01f && Mathf.Abs(lastDir.x) < 0.01f) {
            zAngle = Mathf.Clamp(Mathf.Atan2(lastDir.y, lastDir.x) * Mathf.Rad2Deg, -90, 90);
        } else if (lastDir.y > 0 && lastDir.x > 0) {
            zAngle = Mathf.Clamp(Mathf.Atan2(lastDir.y, lastDir.x) * Mathf.Rad2Deg, 0, 45);
            //zAngle = Mathf.Clamp(Mathf.Atan2(lastDir.y, lastDir.x) * Mathf.Rad2Deg, -45, 45);
            yAngle = 0;
        }
        else if (lastDir.y > 0 && lastDir.x < 0) {
            zAngle = Mathf.Clamp(Mathf.Atan2(lastDir.y, lastDir.x) * Mathf.Rad2Deg, 0, 45);
            yAngle = 180;
        }
        else if (lastDir.y < 0 && lastDir.x > 0) {
            zAngle = Mathf.Clamp(Mathf.Atan2(lastDir.y, lastDir.x) * Mathf.Rad2Deg, -45, 0);
            yAngle = 0;
        } else if(lastDir.y < 0 && lastDir.x < 0) {
            zAngle = Mathf.Clamp(Mathf.Atan2(lastDir.y, lastDir.x) * Mathf.Rad2Deg, -45, 0);
            yAngle = 180;
        }
        
        else {
            zAngle = 0;
        }

        Vector3 targetRotation = new Vector3(transform.eulerAngles.x, yAngle, zAngle);
        Vector3 smothedRotation = new Vector3(targetRotation.x, Mathf.LerpAngle(transform.eulerAngles.y, yAngle, rotationSpeed),Mathf.LerpAngle(transform.eulerAngles.z, zAngle,rotationSpeed));
        transform.rotation = Quaternion.Euler(smothedRotation);
    }

    public void CallTurn(InputAction.CallbackContext context) {
        //if (context.started) Turn();s
    }
}
