using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelsMove : MonoBehaviour{
    public PlayerMovementScroll playerMove;

    private void Start(){
        playerMove = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovementScroll>();
    }

    public float ScrollSpeed;
    private void FixedUpdate(){
        if(playerMove.isAlive)
        transform.Translate(Vector2.left * ScrollSpeed * Time.fixedDeltaTime);
    }
}
