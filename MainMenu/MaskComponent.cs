using iOS4Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskComponent : MonoBehaviour
{

    public float DezintegrationSpeed;
   
    private void FixedUpdate()
    {
        transform.localScale -= new Vector3(Time.fixedDeltaTime *DezintegrationSpeed/100, Time.fixedDeltaTime * DezintegrationSpeed / 100, 0);
        if(transform.localScale.x <= 0.02f){
            Destroy(this.gameObject);
        }
    }
}
