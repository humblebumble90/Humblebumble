using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

public class SkeletonController : MonoBehaviour
{
    private AudioSource deadSound;
    public GameController gc;
    [Header("Speed Values")]
    [SerializeField]
    public Speed horizontalSpeedRange;
    [SerializeField]
    public Speed verticalSpeedRange;
    public float verticalSpeed;
    public float horizontalSpeed;
    [SerializeField]
    public Boundary boundary;
    // Start is called before the first frame update
    void Start()
    {
        GameObject gco = GameObject.FindWithTag("GameController");
        gc = gco.GetComponent<GameController>();
        deadSound = gc.audioSources[(int)SoundClip.Skeletone_Dead];
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
        Vector2 newPosition = new Vector2(horizontalSpeed,verticalSpeed);
        Vector2 currentPosition = transform.position;

        currentPosition -= newPosition;
        transform.position = currentPosition;
    }
    void CheckBounds()
    {
        if (transform.position.y >= boundary.Top + 1.0f
            || transform.position.y <= boundary.Bottom - 1.0f
            || transform.position.x <= boundary.Left -1.0f
            || transform.position.x >= boundary.Right +1.0f
            ) 
            Reset();
    }
    private void Reset()
    {
        if(transform.position.y > 0)
        {
            verticalSpeed = Random.Range(0, verticalSpeedRange.min);
        }
        if(transform.position.y > 0)
        {
            verticalSpeed = Random.Range(0, verticalSpeedRange.max);
        }
        horizontalSpeed = Random.Range(horizontalSpeedRange.min, horizontalSpeedRange.max);
        verticalSpeed = Random.Range(verticalSpeedRange.min, verticalSpeedRange.max);
        float randomYPosition = Random.Range(boundary.Top, boundary.Bottom);
        transform.position = new Vector2(boundary.Right, randomYPosition);
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Fire")
        {
            Destroy(this.gameObject);
            Destroy(col.gameObject);
            gc.Score += 300;
            deadSound.Play();

        }
    }
}
