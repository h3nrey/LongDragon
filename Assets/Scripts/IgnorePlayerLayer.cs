using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnorePlayerLayer : MonoBehaviour
{
    [SerializeField] LayerMask playerMask = 6;
    void FixedUpdate() {
        Physics2D.IgnoreLayerCollision(6, 6);
    }
}
