using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class introHalfBro : MonoBehaviour{

    private float Timer;
    void Start(){
        Timer = 7f;
    }
    private void Update(){
        if (Timer <= 0f){
            SceneManager.LoadScene("halfBrokenGamePlay");
        }
        else{
            Timer -= Time.deltaTime;
        }
            
    }



}
