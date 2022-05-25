using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableFloor : MonoBehaviour {

    private SpriteRenderer spriteRenderer;
    private Sprite breakableFloorSprite1;
    private Sprite breakableFloorSprite2;
    private Sprite breakableFloorSprite3;
    private Sprite breakableFloorSprite4;
    private Transform breakingParticlesPrefab;

    private float timer;
    private bool timeStart;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        breakableFloorSprite1 = Resources.Load<Sprite>("Sprites/BreakableFloor1");
        breakableFloorSprite2 = Resources.Load<Sprite>("Sprites/BreakableFloor2");
        breakableFloorSprite3 = Resources.Load<Sprite>("Sprites/BreakableFloor3");
        breakableFloorSprite4 = Resources.Load<Sprite>("Sprites/BreakableFloor4");

        breakingParticlesPrefab = Resources.Load<Transform>("Prefabs/pfBreakingParticles");
    }

    private void Update() {
        if (timeStart) {
            timer += Time.deltaTime;

            if (timer > 0.5f) {
                spriteRenderer.sprite = breakableFloorSprite2;
            }

            if (timer > 1f) {
                spriteRenderer.sprite = breakableFloorSprite3;
            }

            if (timer > 1.5f) {
                spriteRenderer.sprite = breakableFloorSprite4;
            }

            if (timer > 2f) {
                Instantiate(breakingParticlesPrefab, transform.position, Quaternion.Euler(transform.eulerAngles));
                transform.gameObject.SetActive(false);
            }
        }
    }

    public void ResetFloor() {
        timer = 0f;
        timeStart = false;
        spriteRenderer.sprite = breakableFloorSprite1;
        transform.gameObject.SetActive(true);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.tag == "Ball") {
            timeStart = true;
        }
    }

}
