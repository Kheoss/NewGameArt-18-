using UnityEngine;

public class PlantLogics : MonoBehaviour{

    public GameObject BulletPrefab;

    public float TimeForShoot;
    private bool _atackedThisTime;
    private float Timer;
    public int MaxHealth;
    public int CurHealth;
    private Animator anim;
    public GameObject firePlace;
    private GameObject _player;
    private void Start(){
        _player = GameObject.FindGameObjectWithTag("Player");
        _atackedThisTime = false;
        anim = GetComponent<Animator>();
        CurHealth = MaxHealth;
        Timer = TimeForShoot;
    }
    private void Update(){
        if(Timer > 0){
            Timer -= Time.deltaTime;
            if(Timer < 1.3f && !_atackedThisTime){
                anim.SetTrigger("AttackTrigger");
                _atackedThisTime = true;
            }
        }
        else{
            Timer = TimeForShoot;
            Attack();
            _atackedThisTime = false;
        }
    }
    public void Attack(){
        Vector2 dir = (Vector2)_player.transform.position - (Vector2)firePlace.transform.position;
        dir /= dir.magnitude;
        SpawnBullet(-dir.x,dir.y,20);
    }
    private void SpawnBullet(float x, float y, float speed){
        GameObject gm = Instantiate(BulletPrefab, firePlace.transform.position, Quaternion.identity);
        BulletLogics log = gm.GetComponent<BulletLogics>();
        log.Dimensions = new float[3];
        log.Dimensions[0] = x;
        log.Dimensions[1] = y;
        log.Speed = speed;
        log.RoomName = this.name;
    }
    private void OnTriggerEnter2D(Collider2D col){
        if(col.tag == "Weapon"){
            CurHealth -= 10;
            Camera.main.SendMessage("TriggerShake", 0.5f);
            if(CurHealth <= 0){
                transform.parent.parent.SendMessage("OneDied");
                Destroy(this.gameObject);
            }
        }
    }
    
}
