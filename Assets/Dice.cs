using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour {
    public Sprite[] sprites;
    public SpriteRenderer spriteRenderer;
    private int faceIndex;
    public int index;
    public bool locked;

    private void Start () {
        faceIndex = 1;
        spriteRenderer.sprite = sprites[faceIndex];
    }

    public void Roll () {
        if (locked) return;

        faceIndex = Random.Range (0, 6);
        spriteRenderer.sprite = sprites[faceIndex];
    }

    public int GetFace () {
        return faceIndex + 1;
    }

    private void OnMouseUpAsButton () {
        if (DiceManager.rolling || DiceManager.reseted) return;

        if (locked) {
            Unlock ();
        } else {
            Lock ();
        }
    }

    public void Lock () {
        locked = true;
        transform.localScale = new Vector3 (.3f, .3f, .3f);
    }

    public void Unlock () {
        locked = false;
        transform.localScale = new Vector3 (.45f, .45f, .45f);
    }

}