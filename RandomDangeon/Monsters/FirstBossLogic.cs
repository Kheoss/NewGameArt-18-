using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBossLogic : MonoBehaviour{
    public int maxHealth;
    public int curHealth;
    public GameObject meleHolder;
    public float MeleRotationSpeed;
    private void Start(){
        maxHealth = 100;
        curHealth = maxHealth;
    }
    private void FixedUpdate(){
        meleHolder.transform.eulerAngles = new Vector3(0,0, meleHolder.transform.eulerAngles.z+2);
    }
}
