using UnityEngine;

public class BulletLogics : MonoBehaviour{

    public float Speed;
    public string RoomName;
    public int DMG;

    public float[] Dimensions;
  /* index |   1 | -1
     *_____|_____|______
     * 0   |left | right
     * 1   | top | bottom
     */
    public Vector2 movement;
    private void Start(){
        DMG = 20;
        movement = Vector2.left * Dimensions[0] + Vector2.up * Dimensions[1];
    }

    private void FixedUpdate(){
        transform.Translate(movement * Speed * Time.deltaTime);
    }

    public void OnTriggerEnter2D(Collider2D col){
        if (col.gameObject.tag == this.gameObject.tag) return;
        if (col.gameObject.tag == "Weapon") return;
        if(col.tag == "Player")
            col.SendMessage("ApplyDamage", DMG);
        if (col.name != RoomName && col.tag != "SpawnPoint" && col.gameObject.name != "CameraPoint"){
            Destroy(this.gameObject);
        }
    }
    
}
