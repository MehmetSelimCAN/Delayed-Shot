using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ventilator : MonoBehaviour {

    private float forceSpeed = 15f;

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.tag == "Ball") {
            collision.GetComponent<Rigidbody2D>().AddForce(transform.TransformVector(Vector2.up) * forceSpeed);
        }
    }

}
