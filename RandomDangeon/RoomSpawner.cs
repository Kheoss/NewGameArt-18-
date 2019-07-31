using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{

    public bool isSpawned;
    public int directions;
    public RoomDBBrain brain;
    /*
     * 1->left
     * 2->right
     * 3->top
     * 4->bottom
    */
    private void Start()
    {
        brain = GameObject.FindGameObjectWithTag("RoomManager").GetComponent<RoomDBBrain>();
        isSpawned = false;
        Invoke("Spawn", 0.1f);
    }
    private void Spawn()
    {
        if (isSpawned) return;
        GameObject gm = null;
        int rand;
        switch (directions)
        {
            case 1: rand = Random.Range(0, brain.rightRooms.Length); gm = brain.rightRooms[rand]; break;
            case 2: rand = Random.Range(0, brain.leftRooms.Length); gm = brain.leftRooms[rand]; break;
            case 3: rand = Random.Range(0, brain.bottomRooms.Length); gm = brain.bottomRooms[rand]; break;
            case 4: rand = Random.Range(0, brain.topRooms.Length); gm = brain.topRooms[rand]; break;
        }
        Instantiate(gm, this.gameObject.transform.position, gm.transform.rotation);
        isSpawned = true;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SpawnPoint"))
        {
            RoomSpawner sp = other.GetComponent<RoomSpawner>();

            if (sp &&sp.isSpawned == false && isSpawned == false)
            {
                //Instantiate(.closedRoom, transform.position, Quaternion.identity);
                Destroy(gameObject);
                isSpawned = true;
            }
        }
    }
}
