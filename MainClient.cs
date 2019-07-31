using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainClient : MonoBehaviour
{
    public GameObject maskPrefab;
    private float Timer;
    private float TimeToWait;
    public Animator phoneCallAim;
    private void Start()
    {
        TimeToWait = 0.01f;
        Timer = TimeToWait;
        StartCoroutine(startAnim());
    }
    IEnumerator startAnim(){
        yield return new WaitForSeconds(5f);
        phoneCallAim.SetTrigger("Call");
    }
    void Update(){
        if (Timer <= 0f)
        {
            if (Input.touchCount >= 1)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    Vector3 pos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                    pos.z = 2;
                    Instantiate(maskPrefab, pos, Quaternion.identity);
                }
            }
            Timer = TimeToWait;
        }
        else
        {
            Timer -= Time.deltaTime;
        }
    }
}
