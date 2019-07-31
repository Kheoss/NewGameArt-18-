using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "SpawnPoint")
        {
            Destroy(col.gameObject);
        }
    }
}

