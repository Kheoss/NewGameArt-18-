using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public enum TrapType{
    ROTATING,
    MOVING
    };
public class Traps : MonoBehaviour{
    public TrapType type;
    public PlayerMovementScroll playerMove;
    private void Start()
    {
        playerMove = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovementScroll>();
    }
    private void FixedUpdate(){
        if (!playerMove.isAlive) return;
            if (type == TrapType.ROTATING){
            transform.Rotate(Vector3.forward * 75 * Time.fixedDeltaTime);
        }
       else if(type == TrapType.MOVING){
        transform.Translate(new Vector2(-6.2f * Time.fixedDeltaTime,0));
        }
    }

}
