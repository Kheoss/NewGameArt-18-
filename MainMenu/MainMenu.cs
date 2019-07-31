using iOS4Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject maskPrefab;
    public float TimeToWait;
    private float Timer;
    public Animator anim;
    private void Start(){
        Timer = TimeToWait;
        Screen.autorotateToPortrait = true;
        Screen.autorotateToPortraitUpsideDown = true;
        Screen.autorotateToLandscapeLeft = false;
        Screen.autorotateToLandscapeRight = false;
        Screen.orientation = ScreenOrientation.AutoRotation;
    }
    private void Update(){
        if(Timer <= 0f){
            if (Input.touchCount >= 1){
                if (Input.GetTouch(0).phase == TouchPhase.Moved){
                    Vector3 pos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                    pos.z = 2;
                    Instantiate(maskPrefab, pos, Quaternion.identity);
                }
            }
            Timer = TimeToWait;
        }
        else {
        Timer -= Time.deltaTime;
        }
    }

    public void StartNewGame(){
        StartCoroutine(startGame());
    }
    private IEnumerator startGame(){
        anim.gameObject.SetActive(true);
        anim.SetTrigger("Appear");
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene("IntroScene");
    }

}
