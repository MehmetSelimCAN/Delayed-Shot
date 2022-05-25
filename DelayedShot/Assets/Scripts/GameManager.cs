using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private Transform basket;

    private Transform ball;
    private Vector3 firstPosition;
    public static bool gameStarted;

    private Camera mainCamera;
    private float currentOrthographicSize;
    private float nextOrthographicSize;
    private float lerpValue;
    private float lerpSpeed;

    private Transform timerUI;
    private Transform rotateInstructionsUI;

    private static Transform howToPlayUI;

    private static Transform winScreenUI;

    private static Transform nextLevelButtonUI;

    private void Awake() {
        gameStarted = false;

        basket = GameObject.FindGameObjectWithTag("Basket").transform;
        
        ball = GameObject.FindGameObjectWithTag("Ball").transform;
        firstPosition = ball.position;

        mainCamera = Camera.main;
        lerpValue = 2f;
        lerpSpeed = 6f;

        timerUI = GameObject.Find("Timer").transform;

        rotateInstructionsUI = GameObject.Find("RotateInstructions").transform;

        if (SceneManager.GetActiveScene().buildIndex == 0)
        howToPlayUI = GameObject.Find("HowToPlay").transform;

        if (SceneManager.GetActiveScene().buildIndex == 4) {
            winScreenUI = GameObject.Find("WinScreen").transform;
            winScreenUI.gameObject.SetActive(false);
        }
        else {
            nextLevelButtonUI = GameObject.Find("NextLevelButton").transform;
            nextLevelButtonUI.GetComponent<Button>().onClick.AddListener(() => {
                NextLevel();
            });
            nextLevelButtonUI.gameObject.SetActive(false);
        }
    }

    private void Start() {
        timerUI.gameObject.SetActive(false);
        rotateInstructionsUI.gameObject.SetActive(false);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (gameStarted) {
                StopLevel();
            }
            else {
                StartLevel();
            }

            gameStarted = !gameStarted;
        }

        if (lerpValue <= 1) {
            lerpValue += lerpSpeed * Time.deltaTime;
            mainCamera.orthographicSize = Mathf.Lerp(currentOrthographicSize, nextOrthographicSize, lerpValue);
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            RestartLevel();
        }
    }

    public void StartGame() {
        howToPlayUI.gameObject.SetActive(false);
    }

    private void StartLevel() {
        ball.GetComponent<SaveTrailRenderer>().CreatePastTrail();

        ball.GetComponent<Rigidbody2D>().isKinematic = false;

        timerUI.gameObject.SetActive(true);
        rotateInstructionsUI.gameObject.SetActive(false);

        for (int i = 0; i < basket.childCount; i++) {
            basket.GetChild(i).GetComponent<SpriteRenderer>().color = new Color(1,1,1,0.5f);
        }

        CameraMovement();
    }

    private void StopLevel() {
        ball.GetComponent<SaveTrailRenderer>().CreatePastTrail();

        ball.GetComponent<Rigidbody2D>().isKinematic = true;
        ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        ball.GetComponent<Rigidbody2D>().angularVelocity = 0f;
        ball.position = firstPosition;

        ball.GetComponent<SaveTrailRenderer>().DestroyCurrentTrail();

        timerUI.GetComponent<Timer>().SetTimer(0f);
        timerUI.gameObject.SetActive(false);

        for (int i = 0; i < basket.childCount; i++) {
            basket.GetChild(i).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }

        ResetFloors();

        CameraMovement();
    }

    private void RestartLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void CameraMovement() {
        if (gameStarted) {
            currentOrthographicSize = 7.5f;
            nextOrthographicSize = 8f;
            lerpValue = 0;
        }
        else {
            currentOrthographicSize = 8f;
            nextOrthographicSize = 7.5f;
            lerpValue = 0;
        }
    }

    private void ResetFloors() {
        BreakableFloor[] breakableFloors = GameObject.FindObjectsOfType<BreakableFloor>(true);
        for (int i = 0; i < breakableFloors.Length; i++) {
            breakableFloors[i].GetComponent<BreakableFloor>().ResetFloor();
        }
    }

    public static void Win() {
        if (SceneManager.GetActiveScene().buildIndex == 4) {
            winScreenUI.gameObject.SetActive(true);
        }
        else {
            nextLevelButtonUI.gameObject.SetActive(true);
        }
    }

    public static void NextLevel() {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
