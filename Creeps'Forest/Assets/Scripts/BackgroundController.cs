using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    //Declaration variables for moving backgrounds.
    public float horizontalSpeed = 0.1f;
    public float resetPostion = 0.0f;
    public float resetPoint = -6.4f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CheckBound();
    }

    void Move()//Moving background by vector2(x,y) value. y value is set 0.0f as the background should move horizontally.
    {
        Vector2 newPosition = new Vector2(horizontalSpeed, 0.0f);
        Vector2 currentPosition = transform.position;
        currentPosition -= newPosition;
        transform.position = currentPosition;
    }
    void Reset()//Reset backgrounds' position to reset position.
    {
        transform.position = new Vector2(resetPostion, 0.0f);
    }
    void CheckBound()//Detect current position reaches the reset point.
    {
        if (transform.position.x <= resetPoint)
        {
            Reset();
        }
    }
}
