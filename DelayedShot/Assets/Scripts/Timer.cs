using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour {

    private float timer;
    private TextMeshProUGUI text;
    private TimeSpan timeSpan;
    public static bool canGoNextLevel;
    private Transform basket;


    private void Awake() {
        text = GetComponent<TextMeshProUGUI>();
        basket = GameObject.FindGameObjectWithTag("Basket").transform;
    }

    private void Update() {
        if (GameManager.gameStarted) {
            timer += Time.deltaTime;
            timeSpan = TimeSpan.FromSeconds(timer);
            text.SetText(string.Format("{0:D1}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds));

            if (timer >= 8f) {
                canGoNextLevel = true;
                text.color = Color.yellow;
                for (int i = 0; i < basket.childCount; i++) {
                    basket.GetChild(i).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1f);
                }
            }
        }
    }

    public void SetTimer(float time) {
        timer = time;
        canGoNextLevel = false;
        text.color = Color.white;
    }

}
