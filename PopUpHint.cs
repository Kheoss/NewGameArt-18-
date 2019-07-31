using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpHint : MonoBehaviour
{
    private void Update(){
        if(Input.touchCount >= 1){
            Destroy(this.gameObject);
        }
    }
}
