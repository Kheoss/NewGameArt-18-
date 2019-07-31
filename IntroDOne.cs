using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class IntroDOne : MonoBehaviour
{

    private float Timer;
    private void Start(){
        Timer = 18f;
    }
    void Update(){
        Timer -= Time.deltaTime;
        
        if(Timer <= 0){
            SceneManager.LoadScene("MainScene");
        }

    }
}
