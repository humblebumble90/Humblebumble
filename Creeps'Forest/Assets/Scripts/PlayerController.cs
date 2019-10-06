using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

public class PlayerController : MonoBehaviour
{
    //Author's name: Tom Tsiliopolos, Wallace Balaniuc.
    //Declaration variables for players' functions.
    public Speed speed;
    public Boundary boundary;
    // Start is called before the first frame update
    [Header("Attack Settings")]
    public GameObject fire;
    public GameObject fireSpawn;//The empty game object whose position is always in front of player
    //and spawns fire when specific key for attack is pushed.
    public float fireRate = 0.5f; // Can be adjusted on Unity as it's declared public.
    private float myTime = 0.0f;

    [Header("Reference")]
    public GameController gameController;
    void Start()
    {
        Move();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CheckBounds();
        Fire();
    }
    void Move()//Moves player character when axis value is growing or decreasing by pusing specific keys.
    {
        Vector2 newPosition = transform.position;
        if (Input.GetAxis("Vertical") > 0.0f)
        {
            newPosition += new Vector2(0.0f, speed.max);
        }
        if (Input.GetAxis("Vertical") < 0.0f)
        {
            newPosition += new Vector2(0.0f, speed.min);
        }
        if (Input.GetAxis("Horizontal") > 0.0f)
        {
            newPosition += new Vector2(speed.max, 0.0f);
        }
        if (Input.GetAxis("Horizontal") < 0.0f)
        {
            newPosition += new Vector2(speed.min, 0.0f);
        }
        transform.position = newPosition;

    }
    public void CheckBounds()//Prevent player to move out of the main camera.
    {
        if (transform.position.x < boundary.Left)
        {
            transform.position = new Vector2(boundary.Left, transform.position.y);
        }
        //Check right boundary.
        if (transform.position.x > boundary.Right)
        {
            transform.position = new Vector2(boundary.Right, transform.position.y);
        }
        if (transform.position.y > boundary.Top)
        {
            transform.position = new Vector3(transform.position.x, boundary.Top);
        }
        if (transform.position.y < boundary.Bottom)
        {
            transform.position = new Vector3(transform.position.x, boundary.Bottom);
        }
    }
    void Fire()//Allow player to fire from FireSpawn gameObject.
    {
        myTime += Time.deltaTime;

        if (Input.GetButton("Fire1") && myTime > fireRate)//Setting fire rate.
        {
            Instantiate(fire, fireSpawn.transform.position, fireSpawn.transform.rotation);
            myTime = 0.0f;
            gameController.audioSources[(int)SoundClip.Fire].Play();
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")//Detect enemy gameObjects' collider to spend a life.
        {
            gameController.Lives -= 1;
            Debug.Log("Life decreased");
            if (gameController.Lives == 0)//When left life is 0, colliding with enemy means game over.
            {
                Destroy(this.gameObject);
                gameController.GameOver();
            }

        }
    }
}
