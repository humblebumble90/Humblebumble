using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    //Author's name: Wallace Balaniuc
    public float speed;//Declaration variables for setting fire moving. The speed value can be adjusted by developer on Unity.
    private Rigidbody2D rBody;
    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody2D>(); // Detecting this gameobject's rigid body to move the object with speed.
        rBody.velocity = transform.right * speed;
    }

}
