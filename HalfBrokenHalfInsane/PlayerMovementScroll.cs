using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovementScroll : MonoBehaviour {
    public float ScrollSpeed;
    public GameObject PlayerSkeleton;
    public GameObject TpPointBroken;
    public GameObject TpPointInsane;
    public GameObject SpawnTrapsBroken;
    public GameObject SpawnTrapsInsane;
    public GameObject[] brokenTraps;
    public GameObject[] insaneTraps;
    public ParticleSystem particles;
    public GameObject loseCanvas;
    public GameObject startCanvas;
    public GameObject WinCanvas;
    public float WinTimer;
    public bool isAlive;
    private enum SelfStatus{
        BROKEN,
        INSANE
    };
    private SelfStatus status;
    private void Start(){
        WinTimer = 0f;
        isAlive = true;
        status = SelfStatus.BROKEN;
        StartCoroutine(TrapSpawner());
    }
    private void Update(){
        WinTimer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space)){
            particles.Play();
            Vector2 transportVec = new Vector2();
            switch (status)
            {
                case SelfStatus.BROKEN:
                    transportVec = TpPointInsane.transform.position;
                    status = SelfStatus.INSANE;
                    break;
                case SelfStatus.INSANE:
                    transportVec = TpPointBroken.transform.position;
                    status = SelfStatus.BROKEN;
                    break;
            }
            PlayerSkeleton.transform.position = transportVec;
        }
        if(WinTimer >= 10f){
            isAlive = false;
            WinCanvas.GetComponent<Animator>().SetTrigger("Win");
            StartCoroutine(LoadNextScene());
        }
    }
    IEnumerator LoadNextScene(){
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("DungeonTry");
    }
    IEnumerator TrapSpawner(){
        while (true){
        yield return new WaitForSeconds(1.1f);
        int rnd  = Random.Range(1, 11);
            if(rnd % 2 == 0){
                GameObject prefab = brokenTraps[Random.Range(0, brokenTraps.Length)];
                Instantiate(prefab, SpawnTrapsBroken.transform.position, Quaternion.identity);
            }
            else{
                GameObject prefab = insaneTraps[Random.Range(0, insaneTraps.Length)];
                Instantiate(prefab, SpawnTrapsInsane.transform.position, Quaternion.identity);
            }
        }
    }
    private void GameOver(){
        isAlive = false;
        loseCanvas.SetActive(true);
        loseCanvas.GetComponent<Animator>().SetTrigger("Lose");
    }
    public void ReloadScene(){
        loseCanvas.GetComponent<Animator>().SetTrigger("Transfer");
        StartCoroutine(sceneloader());
    }
    private IEnumerator sceneloader(){
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
