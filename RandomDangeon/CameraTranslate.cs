using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTranslate : MonoBehaviour
{
    public bool wasHere;
    public bool activatedMonsters;
    public List<GameObject> obstacles;
    public bool CustomListed;
    private GameObject gm;
    private PlayerStats playerStats;
    public bool x;
    private void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        wasHere = false;
        if (!CustomListed){
            obstacles = GameObject.FindGameObjectWithTag("RoomManager").GetComponent<RoomDBBrain>().obstaclesInRooms;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player"){
            Camera.main.GetComponent<Rigidbody2D>().MovePosition(this.transform.position);
            if (!activatedMonsters){
                if (!wasHere){
                    playerStats.curRoom = transform;
                    wasHere = true;
                    if (obstacles.Count == 0) return;
                    transform.parent.SendMessage("BlockDoors");
                    gm = Instantiate(obstacles[Random.Range(0, obstacles.Count - 1)], transform.parent.position, Quaternion.identity);
                    if (x){
                        gm.transform.localScale = new Vector2(1, 1);
                        gm.transform.SetParent(this.gameObject.transform.parent);
                       // gm.transform.localPosition = new Vector2(1, -1);
                    }
                    else{
                        gm.transform.SetParent(this.gameObject.transform.parent);
                        gm.transform.localScale = new Vector2(1, 1);
                    }
                }
            }
        }
    }
    public void RemakeRoom(){
        wasHere = false;
        Destroy(gm.gameObject);
    }
}
