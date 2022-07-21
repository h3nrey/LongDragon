using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PartBehaviour : MonoBehaviour {

    public static PartBehaviour instance;
    [SerializeField] public float partSpeed;
    [SerializeField] public Transform target;

    [SerializeField] public Transform rotatePointZ;
    [SerializeField] public Transform rotatePoint;
    Rigidbody2D partRb;
    // Start is called before the first frame update

    private void OnEnable() {
        instance = this;
    }
    void Start()
    {
        partRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        partRb.position = Vector2.MoveTowards(transform.position, target.position, partSpeed * Time.deltaTime);
        Turn();

    }


    void Turn() {
        //print("Turning");
        if (PlayerBehaviour.instance.facingRight == false && this.transform.rotation.eulerAngles.y == 0)
            this.transform.Rotate(rotatePoint.up, 180f);
        else if (PlayerBehaviour.instance.facingRight == true && this.transform.rotation.y == 1) {
            this.transform.Rotate(rotatePoint.up, -180);
        }
    }


    
}
