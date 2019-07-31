using UnityEngine;

public class CameraDone : MonoBehaviour{

    public int NumberOfCreatures;
    private int Remained;

    private void Start(){
        Remained = NumberOfCreatures;
    }
    
    public void OneDied(){
        Remained--;
        if(Remained <= 0){
            Debug.Log("ROOM WON");
        }
    }
}
