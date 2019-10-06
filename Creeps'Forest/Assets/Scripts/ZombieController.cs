using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;
//Author's name: Tom Tsiliopoulos
public class ZombieController : MonoBehaviour
{
    //Declaration for a zombie's behaviour.
    private GameController gc;
    public Boundary boundary;
    public float horizontalSpeed;
    // Start is called before the first frame update
    private AudioSource deadSound;
    void Start()
    {
        //Declaration for GameController object. Without these declaration, null reference exception will occur.
        GameObject gco = GameObject.FindWithTag("GameController");
        gc = gco.GetComponent<GameController>();
        deadSound = gc.audioSources[(int)SoundClip.Zombie_dead];
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CheckBounds();
    }
    void Move()
    {
        Vector2 newPosition = new Vector2(horizontalSpeed, 0.0f);
        Vector2 currentPostion = transform.position;

        currentPostion -= newPosition;
        transform.position = currentPostion;
    }
    void Reset()
    {
        float randomYPosition = Random.Range(boundary.Top, boundary.Bottom);
        transform.position = new Vector2(boundary.Right, randomYPosition);
    }
    void CheckBounds()//Checking a zombie goes out of game screen.
    {
        if (transform.position.y >= boundary.Top
            || transform.position.y <= boundary.Bottom
            || transform.position.x <= boundary.Left - 1.0f
            || transform.position.x >= boundary.Right + 1.0f
            )
            Reset();//The gameobject moves to the right boundary with random Y position. when its position reaches at boundary.
    }
    //Enemy objects detecting the collider of player's fire through its tag and destroy the fire game object and itself.
    //The method also increase score value and make dead sound by invoking GameController Class's methods.
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Fire")
        {
            Destroy(this.gameObject);
            Destroy(col.gameObject);
            gc.Score += 100;
            deadSound.Play();

        }
    }

}
