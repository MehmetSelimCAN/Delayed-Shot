using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour {

    private Vector3 mousePosition;

    private void Update() {
        transform.position = GetMousePosition();
    }

    private Vector3 GetMousePosition() {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;
        return mousePosition;
    }

}
