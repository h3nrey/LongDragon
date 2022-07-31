using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireBehaviour : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag != this.gameObject.tag && other.gameObject.tag != "Player") Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag != this.gameObject.tag && other.gameObject.tag != "Player") Destroy(this.gameObject);
    }
}
