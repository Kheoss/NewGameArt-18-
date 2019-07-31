using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomBlocker : MonoBehaviour
{
    public bool isFinalRoom;
    public List<Animator> gates = new List<Animator>();
    public bool isFirstRoom;

    private void Start(){
        if (!isFirstRoom){
            foreach (Animator anim in gates){
                anim.SetTrigger("Open");
            }
        }
    }
    public void OpenAllDoors(){
        foreach (Animator anim in gates)
        {
            anim.SetTrigger("Open");
        }
    }

    public void RoomUnlock(){
        Debug.Log("Room WON");
        foreach (Animator anim in gates){
            anim.SetTrigger("Open");
        }
        if (isFinalRoom){
            if (SceneManager.GetActiveScene().buildIndex == 4){
                SceneManager.LoadScene("DungeonTry");
            }
        }
    }
    public void BlockDoors(){
        Debug.Log("close the door");
        foreach (Animator anim in gates){
            anim.SetTrigger("Close");
        }
    }
}
