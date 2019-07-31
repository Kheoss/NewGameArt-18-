using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testCol : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other){
        gameObject.transform.parent.gameObject.SendMessage("GameOver");
    }
}
