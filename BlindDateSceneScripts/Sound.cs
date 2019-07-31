using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip myAudio;
    private void Start(){
        audioSource = GameObject.FindGameObjectWithTag("SoundSource").GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Player"){
            audioSource.clip = myAudio;
            audioSource.Play();
            Destroy(this.gameObject);
        }
    }
}
