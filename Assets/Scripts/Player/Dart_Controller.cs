using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dart_Controller : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        Destroy(gameObject);
    }
}