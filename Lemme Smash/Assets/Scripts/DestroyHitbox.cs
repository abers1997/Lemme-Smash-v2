using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyHitbox : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Arrow" || other.gameObject.tag == "ComboBreaker")
        {
            Destroy(other.gameObject);
        }
    }
}
