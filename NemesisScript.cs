using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NemesisScript : MonoBehaviour{

    public float Speed;
    private float Distance;
    private GameObject player;
    private Vector2 inputMovement;
    private Vector2 movement;
    private Rigidbody2D rb;
    public GameObject BloodPrefab;
    private void Start(){
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        Speed = 5f;
    }
    private void Update(){
        Distance = Vector2.Distance(player.transform.position, this.transform.position);
        if(Distance < 35f && Distance > 5f){
            inputMovement = transform.position - player.transform.position;
            movement = inputMovement.normalized * Speed * Time.deltaTime;
            rb.MovePosition(rb.position - movement);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.tag.Equals("Weapon")){
            GameObject.FindGameObjectWithTag("Minimanager").SendMessage("KillOne");
            Die();
        }
    }
    private void Die(){
        Camera.main.SendMessage("TriggerShake", 0.2f);
        Instantiate(BloodPrefab, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}
