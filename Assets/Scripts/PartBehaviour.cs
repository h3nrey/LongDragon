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
}
