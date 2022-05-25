using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorsMovement : MonoBehaviour {

    private Transform selectedObject;
    private Vector3 offset;
    private Transform rotateInstructionsUI;
    private Vector3 mousePosition;
    private Collider2D targetObject;

    private void Awake() {
        rotateInstructionsUI = GameObject.Find("RotateInstructions").transform;
    }

    void Update() {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0)) {
            targetObject = Physics2D.OverlapPoint(mousePosition);
            if (targetObject) {
                selectedObject = targetObject.transform;
                offset = selectedObject.transform.position - mousePosition;
            }
        }

        if (selectedObject != null && selectedObject.tag == "Moveable" && !GameManager.gameStarted) {
            rotateInstructionsUI.gameObject.SetActive(true);
            selectedObject.transform.position = mousePosition + offset;
            
            selectedObject.transform.position = new Vector3(
                                                Mathf.Clamp(selectedObject.transform.position.x, -11f, 11f),
                                                Mathf.Clamp(selectedObject.transform.position.y, -7.5f, 7.5f),
                                                selectedObject.transform.position.z);

            if (Input.GetKey(KeyCode.Q)) {
                selectedObject.transform.Rotate(0f, 0f, 1f);
            }
            else if (Input.GetKey(KeyCode.E)) {
                selectedObject.transform.Rotate(0f, 0f, -1f);
            }

        }

        if (Input.GetMouseButtonUp(0) && selectedObject != null && selectedObject.tag == "Moveable") {
            rotateInstructionsUI.gameObject.SetActive(false);
            selectedObject = null;
        }
    }
}
