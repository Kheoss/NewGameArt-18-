using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberScript : MonoBehaviour {

    public float Speed;
    private GameObject _player;
    public float MaxHealth;
    public float CurHealth;
    public int thisDmg;

    private void Start(){
        thisDmg = 100;
        MaxHealth = 100;
        CurHealth = MaxHealth;
        _player = GameObject.FindGameObjectWithTag("Player");
    }
    private void FixedUpdate(){
      transform.position = Vector2.MoveTowards(transform.position, _player.transform.position,0.3f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player"){
            _player.SendMessage("ApplyDamage", thisDmg);
            Destroy(this.gameObject);
        }
        if(collision.gameObject.tag == "Weapon"){
            CurHealth -= _player.GetComponent<PlayerStats>().Damage;
            if(CurHealth <= 0){
                transform.parent.SendMessage("OneDied");
                Destroy(this.gameObject);
            }
        }
    }


}
