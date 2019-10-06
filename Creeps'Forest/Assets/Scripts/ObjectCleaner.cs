﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCleaner : MonoBehaviour
{
    //Author's name: Wallace Balaniuc
    //Removes generated objects that go out of main camera.
    private void OnTriggerExit2D(Collider2D other)
    {
        Destroy(other.gameObject);
    }
}