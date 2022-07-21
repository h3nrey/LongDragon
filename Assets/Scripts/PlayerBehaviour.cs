using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
    static public PlayerBehaviour instance;
    [SerializeField] public Vector2 input { get; set; }
    [SerializeField] private float horizontalSpeed;

    [Header("Physics")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] public static float velX, velY; // shorthands to rb.velocity in X and Y axis

    [Header("Jump")]
    [SerializeField] float jumpForce = 300f;
    [SerializeField] float flySpeed = 10f;

    [Header("Checks")]
    public bool facingRight = true;
    [SerializeField] private bool pressingFlybutton = false;

    private void Awake() {
        if(!instance) instance = this;
    }


    private void FixedUpdate() {
        velX = rb.velocity.x;
        velY = rb.velocity.y;

        rb.velocity = new Vector2(input.x * horizontalSpeed * Time.fixedDeltaTime, velY);
        Flying();
    }

    private void Update() {
        Turn();
        CheckFacing();
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

    void CheckFacing() {
        if (input.x == 1) facingRight = true; else if(input.x == -1)facingRight = false;
    }

    void Turn() {
        if (facingRight) transform.rotation = Quaternion.Euler(Vector2.up * 0);
        else transform.rotation = Quaternion.Euler(Vector2.up * 180);
    }

    private void Jump() {
        rb.AddForce(Vector2.up * jumpForce);
    }

    private void Flying() {
        if (pressingFlybutton) {
            rb.gravityScale = 0;
            rb.velocity = new Vector2(velX, flySpeed * Time.deltaTime);
            //rb.AddForce(Vector2.up * flySpeed * Time.fixedDeltaTime, ForceMode2D.Impulse);
        } else rb.gravityScale = 1;
    }
}
