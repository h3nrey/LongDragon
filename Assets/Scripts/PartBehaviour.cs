using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PartBehaviour : MonoBehaviour {

    public static PartBehaviour instance;
    [SerializeField] public float partSpeed;
    [SerializeField] private float positionMultiplier;
    [SerializeField] public Transform target;

    [SerializeField] public Transform rotatePointZ;
    [SerializeField] public Transform rotatePoint;
    Rigidbody2D partRb;

    LayerMask playerMask;
    public int partPosition;
    // Start is called before the first frame update

    private void OnEnable() {
        instance = this;
    }
    void Start()
    {
        partRb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        Physics2D.IgnoreLayerCollision(playerMask, playerMask);
    }

    // Update is called once per frame
    //void Update() {
    //    partSpeed = Mathf.Min((Mathf.Abs(Mathf.Abs(PlayerBehaviour.instance.rb.velocity.magnitude) - partPosition * positionMultiplier) * Time.deltaTime * 10), PlayerBehaviour.instance.rb.velocity.magnitude);

    //    partRb.position = Vector2.MoveTowards(transform.position, target.position, partSpeed);
    //    print($"menor valor: {partSpeed}, velocity: {PlayerBehaviour.instance.rb.velocity.magnitude}");
    //    //partRb.velocity = PlayerBehaviour.instance.rb.velocity;


    //    //transform.rotation = Quaternion.Euler(transform.eulerAngles.x,transform.eulerAngles.y, target.eulerAngles.z - (-partPosition ));


    //    Turn();

    //}


    //void Turn() {
    //    //print("Turning");
    //    if (PlayerBehaviour.instance.facingRight == false && this.transform.rotation.eulerAngles.y == 0)
    //        this.transform.Rotate(rotatePoint.up, 180f);
    //    else if (PlayerBehaviour.instance.facingRight == true && this.transform.rotation.y == 1) {
    //        this.transform.Rotate(rotatePoint.up, -180);
    //    }
    //}



}
