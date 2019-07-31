using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomLogic : MonoBehaviour
{
    public int MonsterNr;

  public void OneDied(){
        MonsterNr--;
        if(MonsterNr == 0){
            transform.parent.SendMessage("RoomUnlock");
        }
   }
}
