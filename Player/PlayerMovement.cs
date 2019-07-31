using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour{
    public float Speed;
    private Rigidbody2D rb;
    private Vector2 movement;
    private Vector2 inputMovement;
    private GameObject weapon1;
    private float weaponAngle;
    private PlayerStats stats;
    public Joystick LeftJoyStick;
    public Joystick RightJoyStick;
    public ParticleSystem walkPart;
    public Animator anim;
    private Camera cam;

    private void Start(){
        cam = Camera.main;
        anim = GetComponent<Animator>();
        stats = GetComponent<PlayerStats>();
        weapon1 = this.transform.GetChild(0).gameObject;
        rb = GetComponent<Rigidbody2D>();
    }
    
    private void Update(){
        inputMovement = new Vector2(LeftJoyStick.Horizontal, LeftJoyStick.Vertical);
        movement = inputMovement.normalized * Speed * Time.deltaTime;
        weaponAngle = Mathf.Atan2(RightJoyStick.Vertical, RightJoyStick.Horizontal) * 180 / Mathf.PI;
    }
    private void FixedUpdate(){
        rb.MovePosition(rb.position + movement);
        weapon1.transform.eulerAngles = new Vector3(0, 0, weaponAngle-90);
        if (movement != Vector2.zero){
            anim.SetTrigger("Run");
            if(!walkPart.isPlaying)
            walkPart.Play();
        }else{
            anim.SetTrigger("Stai");
            if (!walkPart.isStopped)
                walkPart.Stop();
        }
    }


}
