using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerStats : MonoBehaviour{

    public int CurHealth;
    public int MaxHealth;
    public int Damage;
    public Transform curRoom;
    private RoomBlocker room;
    public GameObject spawnPoint;
    public List<Image> hearts = new List<Image>();

    private void Start(){
        CurHealth = MaxHealth;
        UpdateHearts();
    }
    public void ApplyDamage(int dmg){
        //room = curRoom.parent.GetComponent<RoomBlocker>();
        CurHealth -= dmg;
        UpdateHearts();
        if (CurHealth <= 0){
            //room.OpenAllDoors();
            //curRoom.GetComponent<CameraTranslate>().RemakeRoom();
            //gameObject.transform.position = spawnPoint.transform.position;
            StartCoroutine(sceneReload());
        }
    }
    private IEnumerator sceneReload(){
        yield return new WaitForSeconds(1);
    SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

public void UpdateHearts(){
        if (CurHealth < 80){
            hearts[4].gameObject.SetActive(false);
        }
        else{
            hearts[4].gameObject.SetActive(true);
        }
        if (CurHealth < 60){
            hearts[3].gameObject.SetActive(false);
        }
        else{
            hearts[3].gameObject.SetActive(true);
        }
        if (CurHealth < 40){
            hearts[2].gameObject.SetActive(false);
        }
        else{
            hearts[2].gameObject.SetActive(true);
        }
        if (CurHealth < 20){
            hearts[1].gameObject.SetActive(false);
        }
        else{
            hearts[1].gameObject.SetActive(true);
        }
    }
}
   